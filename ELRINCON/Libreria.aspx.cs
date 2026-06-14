using System;
using System.Collections.Generic;

namespace ELRINCON
{
    public partial class Libreria : System.Web.UI.Page
    {
        public List<Dominio.Producto> ListaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Negocio.ProductoNegocio negocio = new Negocio.ProductoNegocio();
                ListaProductos = negocio.listarLibreria();
                repProductos.DataSource = ListaProductos;
                repProductos.DataBind();
            }
        }
    }
}
