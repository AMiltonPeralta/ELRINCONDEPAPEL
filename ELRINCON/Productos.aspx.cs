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
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 1. Cargar las categorías dinámicamente desde la base de datos
                    CargarCategorias();

                    // 2. Procesar el parámetro de búsqueda o categoría en la QueryString
                    string busqueda = Request.QueryString["busqueda"];
                    string catId = Request.QueryString["cat"];

                    if (busqueda != null)
                    {
                        // Alternar paneles de visualización
                        pnlSeleccion.Visible = false;
                        pnlListado.Visible = true;

                        // Cargar y filtrar los productos de forma global
                        ProductoNegocio productoNegocio = new ProductoNegocio();
                        List<Producto> listaProductos = productoNegocio.listar();
                        List<Producto> listaAMostrar;

                        if (string.IsNullOrWhiteSpace(busqueda))
                        {
                            lblNombreCategoria.Text = "Productos Recomendados";
                            listaAMostrar = listaProductos != null ? listaProductos.Take(10).ToList() : new List<Producto>();
                        }
                        else
                        {
                            lblNombreCategoria.Text = "Resultados para: \"" + busqueda + "\"";
                            string filtro = busqueda.ToUpper();
                            listaAMostrar = listaProductos.FindAll(x =>
                                x.Nombre.ToUpper().Contains(filtro) ||
                                x.Marca.Nombre.ToUpper().Contains(filtro) ||
                                x.Categoria.Nombre.ToUpper().Contains(filtro)
                            );
                        }

                        if (listaAMostrar != null && listaAMostrar.Count > 0)
                        {
                            repProductos.DataSource = listaAMostrar;
                            repProductos.DataBind();
                            repProductos.Visible = true;
                            pnlSinProductos.Visible = false;
                        }
                        else
                        {
                            repProductos.Visible = false;
                            pnlSinProductos.Visible = true;
                            lnkAgregarProductoVacio.NavigateUrl = "FormularioProducto.aspx";
                        }

                        // Configurar el enlace del botón Agregar Producto
                        lnkAgregarProducto.NavigateUrl = "FormularioProducto.aspx";
                    }
                    else if (!string.IsNullOrEmpty(catId) && catId != "0")
                    {
                        // Alternar paneles de visualización
                        pnlSeleccion.Visible = false;
                        pnlListado.Visible = true;

                        // Buscar el nombre de la categoría actual para mostrarlo en el título
                        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                        List<Categoria> categorias = categoriaNegocio.listar();
                        Categoria seleccionada = categorias.FirstOrDefault(c => c.Id.ToString() == catId);
                        
                        if (seleccionada != null)
                        {
                            lblNombreCategoria.Text = seleccionada.Nombre;
                        }
                        else
                        {
                            lblNombreCategoria.Text = "Desconocida";
                        }

                        // Cargar y filtrar los productos
                        ProductoNegocio productoNegocio = new ProductoNegocio();
                        List<Producto> listaProductos = productoNegocio.listar(idCategoria: catId);

                        if (listaProductos != null && listaProductos.Count > 0)
                        {
                            repProductos.DataSource = listaProductos;
                            repProductos.DataBind();
                            repProductos.Visible = true;
                            pnlSinProductos.Visible = false;
                        }
                        else
                        {
                            repProductos.Visible = false;
                            pnlSinProductos.Visible = true;
                            lnkAgregarProductoVacio.NavigateUrl = "FormularioProducto.aspx?cat=" + catId;
                        }

                        // Configurar el enlace del botón Agregar Producto
                        lnkAgregarProducto.NavigateUrl = "FormularioProducto.aspx?cat=" + catId;
                    }
                }
            }
            catch (Exception ex)
            {
                // En un proyecto real se registraría el error, aquí lo relanzamos
                throw ex;
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> lista = categoriaNegocio.listar();

            ddlCategorias.DataSource = lista;
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataBind();

            // Insertar opción por defecto al inicio
            ddlCategorias.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string seleccion = ddlCategorias.SelectedValue;
            if (seleccion != "0")
            {
                Response.Redirect("Productos.aspx?cat=" + seleccion);
            }
        }

        protected void btnMostrarCrearCat_Click(object sender, EventArgs e)
        {
            pnlCrearCategoria.Visible = true;
            btnMostrarCrearCat.Visible = false;
            txtNuevaCategoria.Text = "";
            txtNuevaCategoria.Focus();
        }

        protected void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            pnlCrearCategoria.Visible = false;
            btnMostrarCrearCat.Visible = true;
        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNuevaCategoria.Text.Trim();
                if (!string.IsNullOrEmpty(nombre))
                {
                    Categoria nueva = new Categoria
                    {
                        Nombre = nombre
                    };

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.agregar(nueva);

                    // Recargar el dropdown para reflejar la nueva categoría
                    CargarCategorias();

                    // Ocultar formulario de creación
                    pnlCrearCategoria.Visible = false;
                    btnMostrarCrearCat.Visible = true;

                    // Seleccionar automáticamente la categoría recién creada
                    // Buscamos el ID recién creado en la lista
                    List<Categoria> listaActualizada = negocio.listar();
                    Categoria creada = listaActualizada.FirstOrDefault(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                    if (creada != null)
                    {
                        ddlCategorias.SelectedValue = creada.Id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                        // Asignamos el precio simulado por categoría
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
                    string busqueda = Request.QueryString["busqueda"];
                    string catId = Request.QueryString["cat"];
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        Response.Redirect("Productos.aspx?busqueda=" + HttpUtility.UrlEncode(busqueda), false);
                    }
                    else if (!string.IsNullOrEmpty(catId))
                    {
                        Response.Redirect("Productos.aspx?cat=" + catId, false);
                    }
                    else
                    {
                        Response.Redirect("Productos.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
