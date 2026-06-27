using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class EnvioNegocio
    {
        public Envio obtenerPorSeguimiento(string numeroSeguimiento)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdEnvio, IdVenta, Direccion, Localidad, CodigoPostal, NumeroSeguimiento, Estado FROM Envios WHERE NumeroSeguimiento = @numeroSeguimiento");
                datos.setearParametro("@numeroSeguimiento", numeroSeguimiento);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Envio envio = new Envio();
                    envio.Id = (int)datos.Lector["IdEnvio"];
                    envio.IdVenta = (int)datos.Lector["IdVenta"];
                    envio.Direccion = (string)datos.Lector["Direccion"];
                    envio.Localidad = (string)datos.Lector["Localidad"];
                    envio.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    envio.NumeroSeguimiento = (string)datos.Lector["NumeroSeguimiento"];
                    envio.Estado = (string)datos.Lector["Estado"];
                    return envio;
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

        public List<Envio> listar()
        {
            List<Envio> lista = new List<Envio>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT E.IdEnvio, E.IdVenta, E.Direccion, E.Localidad, E.CodigoPostal, E.NumeroSeguimiento, E.Estado, 
                           V.NumeroFactura, V.Fecha, U.Nombre as NombreCliente
                    FROM Envios E
                    INNER JOIN Ventas V ON E.IdVenta = V.IdVenta
                    INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Envio envio = new Envio();
                    envio.Id = (int)datos.Lector["IdEnvio"];
                    envio.IdVenta = (int)datos.Lector["IdVenta"];
                    envio.Direccion = (string)datos.Lector["Direccion"];
                    envio.Localidad = (string)datos.Lector["Localidad"];
                    envio.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    envio.NumeroSeguimiento = datos.Lector["NumeroSeguimiento"] != DBNull.Value ? (string)datos.Lector["NumeroSeguimiento"] : "";
                    envio.Estado = (string)datos.Lector["Estado"];
                    
                    // Propiedades auxiliares
                    envio.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    envio.ClienteNombre = (string)datos.Lector["NombreCliente"];
                    envio.FechaVenta = (DateTime)datos.Lector["Fecha"];

                    lista.Add(envio);
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

        public void actualizar(Envio envio)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Envios SET NumeroSeguimiento = @numeroSeguimiento, Estado = @estado WHERE IdEnvio = @idEnvio");
                datos.setearParametro("@numeroSeguimiento", string.IsNullOrEmpty(envio.NumeroSeguimiento) ? (object)DBNull.Value : envio.NumeroSeguimiento);
                datos.setearParametro("@estado", envio.Estado);
                datos.setearParametro("@idEnvio", envio.Id);
                
                datos.ejecutarAccion();
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
