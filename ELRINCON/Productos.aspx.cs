using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELRINCON
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string seleccion = ddlCategorias.SelectedValue;
            if (seleccion == "1")
                Response.Redirect("Libreria.aspx");
            else if (seleccion == "2")
                Response.Redirect("Computacion.aspx");
            else if (seleccion == "3")
                Response.Redirect("Gaming.aspx");
            else if (seleccion == "4")
                Response.Redirect("JuegosMesa.aspx");
        }
    }
}