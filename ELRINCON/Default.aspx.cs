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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    List<Producto> listaCompleta = negocio.listar();
                    
                    // Tomar los primeros 12 productos
                    List<Producto> listaDestacados = listaCompleta != null ? listaCompleta.Take(12).ToList() : new List<Producto>();

                    repProductos.DataSource = listaDestacados;
                    repProductos.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
        }

        protected void repProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                try
                {
                    string idProducto = e.CommandArgument.ToString();

                    // 1. Obtener la lista del carrito desde la sesión (crearla si no existe)
                    List<ItemCarrito> carrito;
                    if (Session["Carrito"] != null)
                    {
                        carrito = (List<ItemCarrito>)Session["Carrito"];
                    }
                    else
                    {
                        carrito = new List<ItemCarrito>();
                    }

                    // 2. Comprobar si el producto ya está en el carrito
                    ItemCarrito itemExistente = carrito.FirstOrDefault(x => x.Producto.Id.ToString() == idProducto);

                    if (itemExistente != null)
                    {
                        // Si ya está, aumentamos la cantidad
                        itemExistente.Cantidad++;
                    }
                    else
                    {
                        // Si no está, lo buscamos de la base de datos y lo agregamos
                        ProductoNegocio prodNegocio = new ProductoNegocio();
                        Producto prod = prodNegocio.listar(idProducto)[0];

                        // Asignamos el precio simulado por categoría (igual que en Productos.aspx.cs)
                        decimal precioSimulado = 1500;
                        if (prod.Categoria.Id == 1) precioSimulado = 1200; // Librería
                        else if (prod.Categoria.Id == 2) precioSimulado = 25000; // Computación
                        else if (prod.Categoria.Id == 3) precioSimulado = 18000; // Gaming
                        else if (prod.Categoria.Id == 4) precioSimulado = 6500; // Juegos de mesa

                        ItemCarrito nuevoItem = new ItemCarrito
                        {
                            Producto = prod,
                            Cantidad = 1,
                            PrecioUnitario = precioSimulado
                        };

                        carrito.Add(nuevoItem);
                    }

                    // 3. Guardar el carrito en la sesión
                    Session["Carrito"] = carrito;

                    // 4. Redirigir para refrescar la pantalla y el badge del navbar
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
        }
    }
}
