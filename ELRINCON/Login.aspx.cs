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
            if (!IsPostBack)
            {
                pnlLogin.Visible = true;
                pnlRegistro.Visible = false;

                if (Session["usuario"] != null)
                {
                    RedireccionarLogueado();
                }

                if (Request.QueryString["admin"] != null && Request.QueryString["admin"].ToString() == "true")
                {
                    lblTituloLogin.Text = "Ingresar como Administrador";
                }
            }
        }

        protected void btnMostrarRegistro_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegistro.Visible = true;
            pnlLoginError.Visible = false;
        }

        protected void btnMostrarLogin_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = true;
            pnlRegistro.Visible = false;
            pnlLoginError.Visible = false;
        }

        protected void btnLoginTradicional_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
            {
                MostrarError("Debes completar tu correo electrónico y contraseña.");
                return;
            }

            RealizarLogin(email, clave);
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtRegNombre.Text.Trim();
            string email = txtRegEmail.Text.Trim();
            string clave = txtRegClave.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
            {
                MostrarError("Todos los campos del formulario de registro son obligatorios.");
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Validar si el correo electrónico ya existe
                if (negocio.existeEmail(email))
                {
                    MostrarError("El correo electrónico ingresado ya se encuentra registrado.");
                    return;
                }

                string rol = "Cliente";
                if (Request.QueryString["admin"] != null && Request.QueryString["admin"].ToString() == "true")
                {
                    rol = "Administrador";
                }

                Usuario nuevo = new Usuario
                {
                    Nombre = nombre,
                    Email = email,
                    Clave = clave,
                    Rol = rol,
                    Activo = true
                };

                // Guardar en la base de datos
                int nuevoId = negocio.registrar(nuevo);
                nuevo.Id = nuevoId;

                // Iniciar sesión automáticamente
                Session.Add("usuario", nuevo);

                RedireccionarLogueado();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
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
                    RedireccionarLogueado();
                }
                else
                {
                    MostrarError("El correo electrónico o la contraseña ingresados son incorrectos.");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private void RedireccionarLogueado()
        {
            if (Request.QueryString["checkout"] != null && Request.QueryString["checkout"].ToString() == "true")
            {
                Response.Redirect("Checkout.aspx", false);
            }
            else
            {
                if ((Request.QueryString["admin"] != null && Request.QueryString["admin"].ToString() == "true") || Seguridad.esAdmin(Session["usuario"]))
                {
                    Response.Redirect("Productos.aspx", false);
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            Context.ApplicationInstance.CompleteRequest();
        }

        private void MostrarError(string mensaje)
        {
            pnlLoginError.Visible = true;
            lblLoginErrorMessage.Text = mensaje;
        }
    }
}
