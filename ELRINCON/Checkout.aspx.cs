using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ELRINCON
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Validar que el carrito no esté vacío
                if (Session["Carrito"] == null || ((List<ItemCarrito>)Session["Carrito"]).Count == 0)
                {
                    Response.Redirect("Carrito.aspx", false);
                    return;
                }

                List<ItemCarrito> carrito = (List<ItemCarrito>)Session["Carrito"];
                int totalItems = carrito.Sum(x => x.Cantidad);
                decimal totalPagar = carrito.Sum(x => x.Subtotal);

                lblCantidadItems.Text = totalItems.ToString();
                lblSubtotal.Text = "$" + string.Format("{0:N2}", totalPagar);
                lblTotal.Text = "$" + string.Format("{0:N2}", totalPagar);

                if (!IsPostBack)
                {
                    CargarMetodosPago();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private void CargarMetodosPago()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdMetodoPago, Nombre FROM MetodosPago WHERE Activo = 1");
                datos.ejecutarLectura();
                ddlMetodoPago.DataSource = datos.Lector;
                ddlMetodoPago.DataValueField = "IdMetodoPago";
                ddlMetodoPago.DataTextField = "Nombre";
                ddlMetodoPago.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validación Personalizada en C# usando la clase Validacion de la capa de Negocio
                if (Validacion.validaTextoVacio(txtDireccion) || 
                    Validacion.validaTextoVacio(txtLocalidad) || 
                    Validacion.validaTextoVacio(txtCodigoPostal))
                {
                    Session.Add("error", "Debes completar todos los campos del formulario de envío para poder continuar.");
                    Response.Redirect("Error.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Éxito temporal (redirección al inicio) antes de implementar el guardado en base de datos
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Capturar la excepción esperada de la redirección
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
