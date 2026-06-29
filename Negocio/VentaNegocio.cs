using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        public int agregar(Venta nueva)
        {
            string connectionString = ConfigurationManager.AppSettings["cadenaConexion"];
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar Venta
                        string queryVenta = @"
                            INSERT INTO Ventas (NumeroFactura, IdUsuario, Fecha, Total, IdMetodoPago) 
                            VALUES (@NumeroFactura, @IdUsuario, @Fecha, @Total, @IdMetodoPago);
                            SELECT CAST(scope_identity() AS int);";

                        int idVenta;
                        using (SqlCommand cmdVenta = new SqlCommand(queryVenta, conexion, transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue("@NumeroFactura", nueva.NumeroFactura);
                            cmdVenta.Parameters.AddWithValue("@IdUsuario", nueva.Cliente.Id);
                            cmdVenta.Parameters.AddWithValue("@Fecha", nueva.Fecha);
                            cmdVenta.Parameters.AddWithValue("@Total", nueva.Total);
                            cmdVenta.Parameters.AddWithValue("@IdMetodoPago", nueva.Pago.Id);

                            idVenta = (int)cmdVenta.ExecuteScalar();
                        }

                        nueva.Id = idVenta;

                        // 2. Insertar Detalles de la Venta
                        string queryDetalle = @"
                            INSERT INTO DetalleVentas (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) 
                            VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal);";

                        foreach (var detalle in nueva.Detalles)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                                cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.Articulo.Id);
                                cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                                cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // 3. Insertar Envío
                        string queryEnvio = @"
                            INSERT INTO Envios (IdVenta, Direccion, Localidad, CodigoPostal, NumeroSeguimiento, Estado, Telefono, Dni) 
                            VALUES (@IdVenta, @Direccion, @Localidad, @CodigoPostal, @NumeroSeguimiento, @Estado, @Telefono, @Dni);";

                        using (SqlCommand cmdEnvio = new SqlCommand(queryEnvio, conexion, transaccion))
                        {
                            cmdEnvio.Parameters.AddWithValue("@IdVenta", idVenta);
                            cmdEnvio.Parameters.AddWithValue("@Direccion", nueva.DatosEnvio.Direccion);
                            cmdEnvio.Parameters.AddWithValue("@Localidad", nueva.DatosEnvio.Localidad);
                            cmdEnvio.Parameters.AddWithValue("@CodigoPostal", nueva.DatosEnvio.CodigoPostal);
                            cmdEnvio.Parameters.AddWithValue("@NumeroSeguimiento", nueva.DatosEnvio.NumeroSeguimiento);
                            cmdEnvio.Parameters.AddWithValue("@Estado", nueva.DatosEnvio.Estado);
                            cmdEnvio.Parameters.AddWithValue("@Telefono", nueva.DatosEnvio.Telefono ?? (object)DBNull.Value);
                            cmdEnvio.Parameters.AddWithValue("@Dni", nueva.DatosEnvio.Dni ?? (object)DBNull.Value);

                            cmdEnvio.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return idVenta;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public Venta buscarPorId(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT V.IdVenta, V.NumeroFactura, V.Fecha, V.Total, V.IdMetodoPago, M.Nombre as MetodoPagoNombre
                    FROM Ventas V
                    INNER JOIN MetodosPago M ON V.IdMetodoPago = M.IdMetodoPago
                    WHERE V.IdVenta = @idVenta");
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["IdVenta"];
                    venta.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.Pago = new MetodoPago
                    {
                        Id = (int)datos.Lector["IdMetodoPago"],
                        Nombre = (string)datos.Lector["MetodoPagoNombre"]
                    };

                    datos.cerrarConexion();

                    venta.Detalles = listarDetalles(venta.Id);
                    return venta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<DetalleVenta> listarDetalles(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT D.IdDetalleVenta, D.Cantidad, D.PrecioUnitario, D.Subtotal, P.IdProducto, P.Nombre, P.ImagenUrl
                    FROM DetalleVentas D
                    INNER JOIN Productos P ON D.IdProducto = P.IdProducto
                    WHERE D.IdVenta = @idVenta");
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta det = new DetalleVenta();
                    det.Cantidad = (int)datos.Lector["Cantidad"];
                    det.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    det.Articulo = new Producto();
                    det.Articulo.Id = (int)datos.Lector["IdProducto"];
                    det.Articulo.Nombre = (string)datos.Lector["Nombre"];
                    det.Articulo.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "";

                    lista.Add(det);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Venta> listarPorUsuario(int idUsuario)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT V.IdVenta, V.NumeroFactura, V.Fecha, V.Total, V.IdMetodoPago, M.Nombre as MetodoPagoNombre,
                           E.IdEnvio, E.NumeroSeguimiento, E.Estado as EstadoEnvio
                    FROM Ventas V
                    INNER JOIN MetodosPago M ON V.IdMetodoPago = M.IdMetodoPago
                    LEFT JOIN Envios E ON V.IdVenta = E.IdVenta
                    WHERE V.IdUsuario = @idUsuario
                    ORDER BY V.Fecha DESC");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["IdVenta"];
                    venta.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.Pago = new MetodoPago
                    {
                        Id = (int)datos.Lector["IdMetodoPago"],
                        Nombre = (string)datos.Lector["MetodoPagoNombre"]
                    };

                    if (datos.Lector["IdEnvio"] != DBNull.Value)
                    {
                        venta.DatosEnvio = new Envio
                        {
                            Id = (int)datos.Lector["IdEnvio"],
                            NumeroSeguimiento = datos.Lector["NumeroSeguimiento"] != DBNull.Value ? (string)datos.Lector["NumeroSeguimiento"] : "",
                            Estado = (string)datos.Lector["EstadoEnvio"]
                        };
                    }

                    lista.Add(venta);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
