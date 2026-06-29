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

        public int registrar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Usuarios (Nombre, Email, Clave, Rol, Activo) 
                    VALUES (@nombre, @email, @clave, @rol, 1);
                    SELECT CAST(scope_identity() AS int);");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@clave", nuevo.Clave);
                datos.setearParametro("@rol", nuevo.Rol);

                return datos.ejecutarAccionScalar();
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

        public bool existeEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM Usuarios WHERE Email = @email");
                datos.setearParametro("@email", email);
                int count = datos.ejecutarAccionScalar();
                return count > 0;
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
