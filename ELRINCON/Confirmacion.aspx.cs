using System;
using System.Web;
using System.Web.UI;

namespace ELRINCON
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["seguimiento"] != null)
            {
                lblSeguimiento.Text = Request.QueryString["seguimiento"].ToString();
            }
            else
            {
                lblSeguimiento.Text = "No disponible";
            }

            // Si es pago fácil (metodo == 4)
            if (Request.QueryString["metodo"] != null && Request.QueryString["metodo"].ToString() == "4")
            {
                pnlConfirmacionPagoFacil.Visible = true;
                if (Request.QueryString["factura"] != null)
                {
                    lblFactura.Text = Request.QueryString["factura"].ToString();
                }
                else
                {
                    lblFactura.Text = "No disponible";
                }
            }
            else
            {
                pnlConfirmacionPagoFacil.Visible = false;
            }
        }
    }
}
