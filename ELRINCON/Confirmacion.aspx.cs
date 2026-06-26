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
        }
    }
}
