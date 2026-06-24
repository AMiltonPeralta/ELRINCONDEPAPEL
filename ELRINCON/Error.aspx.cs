using System;
using System.Web;
using System.Web.UI;

namespace ELRINCON
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["error"] != null)
            {
                lblError.Text = Session["error"].ToString();
            }
            else
            {
                lblError.Text = "Ha ocurrido un error inesperado.";
            }
        }
    }
}
