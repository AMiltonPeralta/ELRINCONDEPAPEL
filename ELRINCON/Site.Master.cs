using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Newtonsoft.Json;

namespace ELRINCON
{
    public partial class SiteMaster : MasterPage
    {
        public int CantidadCarrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string busqueda = Request.QueryString["busqueda"];
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        txtBuscarNav.Text = busqueda;
                    }

                    try
                    {
                        ProductoNegocio prodNegocio = new ProductoNegocio();
                        List<Producto> todos = prodNegocio.listar();
                        repSugerencias.DataSource = todos;
                        repSugerencias.DataBind();
                    }
                    catch (Exception)
                    {
                        // Evitar fallas en carga inicial
                    }
                }

                // Calcular la cantidad total de artículos en el carrito
                if (Session["Carrito"] != null)
                {
                    List<ItemCarrito> carrito = (List<ItemCarrito>)Session["Carrito"];
                    CantidadCarrito = carrito.Sum(x => x.Cantidad);
                }
                else
                {
                    CantidadCarrito = 0;
                }
            }
            catch (Exception)
            {
                CantidadCarrito = 0;
            }
        }

        protected void btnBuscarNav_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscarNav.Text.Trim();
            Response.Redirect("~/Productos.aspx?busqueda=" + HttpUtility.UrlEncode(busqueda), false);
        }

        protected void txtBuscarNav_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBuscarNav.Text.Trim();
            Response.Redirect("~/Productos.aspx?busqueda=" + HttpUtility.UrlEncode(busqueda), false);
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
