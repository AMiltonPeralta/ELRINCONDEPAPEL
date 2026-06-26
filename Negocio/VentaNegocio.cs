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
                            INSERT INTO Envios (IdVenta, Direccion, Localidad, CodigoPostal, NumeroSeguimiento, Estado) 
                            VALUES (@IdVenta, @Direccion, @Localidad, @CodigoPostal, @NumeroSeguimiento, @Estado);";

                        using (SqlCommand cmdEnvio = new SqlCommand(queryEnvio, conexion, transaccion))
                        {
                            cmdEnvio.Parameters.AddWithValue("@IdVenta", idVenta);
                            cmdEnvio.Parameters.AddWithValue("@Direccion", nueva.DatosEnvio.Direccion);
                            cmdEnvio.Parameters.AddWithValue("@Localidad", nueva.DatosEnvio.Localidad);
                            cmdEnvio.Parameters.AddWithValue("@CodigoPostal", nueva.DatosEnvio.CodigoPostal);
                            cmdEnvio.Parameters.AddWithValue("@NumeroSeguimiento", nueva.DatosEnvio.NumeroSeguimiento);
                            cmdEnvio.Parameters.AddWithValue("@Estado", nueva.DatosEnvio.Estado);

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
    }
}
