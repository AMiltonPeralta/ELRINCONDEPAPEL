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
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                    return (int)ViewState["CurrentPage"];
                return 0;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        public int TotalPages
        {
            get
            {
                if (ViewState["TotalPages"] != null)
                    return (int)ViewState["TotalPages"];
                return 0;
            }
            set
            {
                ViewState["TotalPages"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Controlar visibilidad de opciones de edición por rol de usuario
                lnkAgregarProducto.Visible = Seguridad.esAdmin(Session["usuario"]);
                btnMostrarCrearCat.Visible = Seguridad.esAdmin(Session["usuario"]);
                btnEliminarCat.Visible = Seguridad.esAdmin(Session["usuario"]);

                if (!IsPostBack)
                {
                    CargarCategorias();
                    CurrentPage = 0;
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarProductos()
        {
            string busqueda = Request.QueryString["busqueda"];
            string catId = Request.QueryString["cat"];

            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> listaProductos = null;

            if (busqueda != null)
            {
                pnlSeleccion.Visible = false;
                pnlListado.Visible = true;
                btnVolverSeleccion.Visible = true;

                List<Producto> listaCompleta = productoNegocio.listar();
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    lblNombreCategoria.Text = "Productos Recomendados";
                    listaProductos = listaCompleta;
                }
                else
                {
                    lblNombreCategoria.Text = "Resultados para: \"" + busqueda + "\"";
                    string filtro = busqueda.ToUpper();
                    listaProductos = listaCompleta.FindAll(x =>
                        x.Nombre.ToUpper().Contains(filtro) ||
                        x.Marca.Nombre.ToUpper().Contains(filtro) ||
                        x.Categoria.Nombre.ToUpper().Contains(filtro)
                    );
                }
            }
            else if (!string.IsNullOrEmpty(catId) && catId != "0")
            {
                pnlSeleccion.Visible = false;
                pnlListado.Visible = true;
                btnVolverSeleccion.Visible = true;

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.listar();
                Categoria seleccionada = categorias.FirstOrDefault(c => c.Id.ToString() == catId);
                lblNombreCategoria.Text = seleccionada != null ? seleccionada.Nombre : "Desconocida";

                listaProductos = productoNegocio.listar(idCategoria: catId);
            }
            else
            {
                pnlSeleccion.Visible = true;
                pnlListado.Visible = true;
                btnVolverSeleccion.Visible = false;

                lblNombreCategoria.Text = "Todos los productos";
                listaProductos = productoNegocio.listar();
            }

            // Configurar el enlace del botón Agregar Producto
            string currentCat = !string.IsNullOrEmpty(catId) ? catId : "";
            lnkAgregarProducto.NavigateUrl = "FormularioProducto.aspx" + (currentCat != "" ? "?cat=" + currentCat : "");
            lnkAgregarProductoVacio.NavigateUrl = "FormularioProducto.aspx" + (currentCat != "" ? "?cat=" + currentCat : "");

            if (listaProductos != null && listaProductos.Count > 0)
            {
                repProductos.Visible = true;
                pnlSinProductos.Visible = false;
                pnlPaginacion.Visible = true;

                // Configurar PagedDataSource
                PagedDataSource pagedData = new PagedDataSource();
                pagedData.DataSource = listaProductos;
                pagedData.AllowPaging = true;
                pagedData.PageSize = 8; // 8 productos por página

                TotalPages = pagedData.PageCount;

                if (CurrentPage >= TotalPages)
                {
                    CurrentPage = 0;
                }
                pagedData.CurrentPageIndex = CurrentPage;

                btnAnterior.Enabled = !pagedData.IsFirstPage;
                btnSiguiente.Enabled = !pagedData.IsLastPage;

                repProductos.DataSource = pagedData;
                repProductos.DataBind();

                // Llenar números de páginas
                List<int> paginas = new List<int>();
                for (int i = 0; i < TotalPages; i++)
                {
                    paginas.Add(i);
                }
                repPaginas.DataSource = paginas;
                repPaginas.DataBind();
            }
            else
            {
                repProductos.Visible = false;
                pnlPaginacion.Visible = false;
                pnlSinProductos.Visible = true;
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

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                CargarProductos();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages - 1)
            {
                CurrentPage++;
                CargarProductos();
            }
        }

        protected void repPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CambiarPagina")
            {
                CurrentPage = int.Parse(e.CommandArgument.ToString());
                CargarProductos();
            }
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
            pnlConfirmarEliminarCat.Visible = false;
            txtNuevaCategoria.Text = "";
            txtNuevaCategoria.Focus();
            lblMensajeCat.Text = "";
        }

        protected void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            pnlCrearCategoria.Visible = false;
            btnMostrarCrearCat.Visible = true;
            lblMensajeCat.Text = "";
        }

        protected void btnEliminarCat_Click(object sender, EventArgs e)
        {
            lblMensajeCat.Text = "";
            string seleccion = ddlCategorias.SelectedValue;
            if (seleccion == "0")
            {
                lblMensajeCat.Text = "Debe seleccionar una categoría para eliminar.";
                return;
            }

            pnlConfirmarEliminarCat.Visible = true;
            pnlCrearCategoria.Visible = false;
            btnMostrarCrearCat.Visible = true;
            lblCatAEliminar.Text = ddlCategorias.SelectedItem.Text;
            chkConfirmaEliminarCat.Checked = false;
        }

        protected void btnCancelarEliminarCat_Click(object sender, EventArgs e)
        {
            pnlConfirmarEliminarCat.Visible = false;
            lblMensajeCat.Text = "";
        }

        protected void btnConfirmaEliminarCat_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeCat.Text = "";
                if (chkConfirmaEliminarCat.Checked)
                {
                    string idCatStr = ddlCategorias.SelectedValue;
                    if (idCatStr != "0")
                    {
                        int idCat = int.Parse(idCatStr);
                        CategoriaNegocio negocio = new CategoriaNegocio();
                        negocio.eliminar(idCat);

                        // Recargar el dropdown
                        CargarCategorias();

                        pnlConfirmarEliminarCat.Visible = false;
                    }
                }
                else
                {
                    lblMensajeCat.Text = "Debe marcar la casilla para confirmar la eliminación.";
                }
            }
            catch (Exception ex)
            {
                lblMensajeCat.Text = "Error al eliminar la categoría: " + ex.Message;
            }
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

                    // Ocultar formulario de creación y de eliminación
                    pnlCrearCategoria.Visible = false;
                    pnlConfirmarEliminarCat.Visible = false;
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

                        ItemCarrito nuevoItem = new ItemCarrito
                        {
                            Producto = prod,
                            Cantidad = 1,
                            PrecioUnitario = prod.Precio
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
