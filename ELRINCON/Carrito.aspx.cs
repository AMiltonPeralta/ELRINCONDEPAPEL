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
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            List<ItemCarrito> lista = (List<ItemCarrito>)Session["Carrito"];

            if (lista != null && lista.Count > 0)
            {
                pnlCarritoVacio.Visible = false;
                pnlCarritoConItems.Visible = true;

                repItems.DataSource = lista;
                repItems.DataBind();

                // Calcular totales
                decimal total = lista.Sum(x => x.Subtotal);
                lblSubtotal.Text = "$" + string.Format("{0:N2}", total);
                lblTotal.Text = "$" + string.Format("{0:N2}", total);
            }
            else
            {
                pnlCarritoVacio.Visible = true;
                pnlCarritoConItems.Visible = false;
            }
        }

        protected void repItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string idProducto = e.CommandArgument.ToString();
                List<ItemCarrito> lista = (List<ItemCarrito>)Session["Carrito"];

                if (lista == null) return;

                ItemCarrito item = lista.FirstOrDefault(x => x.Producto.Id.ToString() == idProducto);

                if (item != null)
                {
                    if (e.CommandName == "SumarCantidad")
                    {
                        item.Cantidad++;
                    }
                    else if (e.CommandName == "RestarCantidad")
                    {
                        if (item.Cantidad > 1)
                        {
                            item.Cantidad--;
                        }
                        else
                        {
                            // Si la cantidad es 1 y resta, se elimina del carrito
                            lista.Remove(item);
                        }
                    }
                    else if (e.CommandName == "EliminarItem")
                    {
                        lista.Remove(item);
                    }

                    // Guardar de nuevo en la sesión
                    Session["Carrito"] = lista;

                    // Redirigir a Carrito.aspx refresca todo el navbar y el listado de forma limpia
                    Response.Redirect("Carrito.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx?checkout=true", false);
            }
            else
            {
                Response.Redirect("Checkout.aspx", false);
            }
        }
    }
}
