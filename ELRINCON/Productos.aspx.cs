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

                    // 2. Procesar el parámetro de categoría en la QueryString
                    string catId = Request.QueryString["cat"];
                    if (!string.IsNullOrEmpty(catId) && catId != "0")
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
    }
}
