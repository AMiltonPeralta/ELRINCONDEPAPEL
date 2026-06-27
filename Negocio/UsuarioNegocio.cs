using System;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Rol, Activo FROM Usuarios WHERE Email = @email AND Clave = @clave AND Activo = 1");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@clave", usuario.Clave);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Rol = (string)datos.Lector["Rol"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                    return true;
                }
                return false;
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
