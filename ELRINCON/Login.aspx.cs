using System;
using System.Web;
using System.Web.UI;
using Dominio;
using Negocio;

namespace ELRINCON
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya está logueado, redirige a Default
            if (Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnIngresarUsuario_Click(object sender, EventArgs e)
        {
            RealizarLogin("cliente@elrincon.com", "cliente123");
        }

        protected void btnIngresarAdmin_Click(object sender, EventArgs e)
        {
            RealizarLogin("admin@elrincon.com", "admin123");
        }

        private void RealizarLogin(string email, string clave)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Email = email,
                    Clave = clave
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                if (negocio.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Session.Add("error", "Error al intentar iniciar sesión con las credenciales de prueba.");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
