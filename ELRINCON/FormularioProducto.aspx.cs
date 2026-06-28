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
    public partial class FormularioProducto : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmaEliminacion = false;
            try
            {
                // Validar permisos de administrador
                if (!Seguridad.esAdmin(Session["usuario"]))
                {
                    Session.Add("error", "No tienes permisos de administrador para ingresar a esta pantalla.");
                    Response.Redirect("Error.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (!IsPostBack)
                {
                    // Imagen por defecto inicial
                    imgProducto.ImageUrl = "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80";

                    // 1. Cargar las marcas desde la base de datos
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    ddlMarca.DataSource = marcaNegocio.listar();
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataBind();

                    // 2. Cargar las categorías desde la base de datos
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    ddlCategoria.DataSource = categoriaNegocio.listar();
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataBind();

                    // 3. Opción B: Configurar categoría inicial y enlaces si es Alta o Modificación
                    string id = Request.QueryString["id"];
                    string catId = Request.QueryString["cat"];

                    if (!string.IsNullOrEmpty(id))
                    {
                        ProductoNegocio productoNegocio = new ProductoNegocio();
                        Producto seleccionado = (productoNegocio.listar(id))[0];

                        // Pre-cargar los campos
                        txtId.Text = id;
                        txtNombre.Text = seleccionado.Nombre;
                        txtStock.Text = seleccionado.StockActual.ToString();
                        txtImagenUrl.Text = seleccionado.ImagenUrl;
                        if (string.IsNullOrEmpty(seleccionado.ImagenUrl))
                        {
                            imgProducto.ImageUrl = "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80";
                        }
                        else
                        {
                            imgProducto.ImageUrl = seleccionado.ImagenUrl;
                        }
                        ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
                        ddlCategoria.Enabled = false; // Deshabilitar cambio de categoría en edición

                        // Configurar enlace cancelar dinámico
                        string catSelected = seleccionado.Categoria.Id.ToString();
                        lnkCancelar.NavigateUrl = "Productos.aspx?cat=" + catSelected;
                    }
                    else if (!string.IsNullOrEmpty(catId))
                    {
                        // Autoseleccionar la categoría de origen en Alta
                        ddlCategoria.SelectedValue = catId;
                        ddlCategoria.Enabled = false;

                        // Configurar el enlace de Cancelar dinámicamente según la categoría de origen
                        lnkCancelar.NavigateUrl = "Productos.aspx?cat=" + catId;
                    }
                    else
                    {
                        // Por defecto, si entra directo sin parámetros, vuelve a la selección de categorías
                        lnkCancelar.NavigateUrl = "Productos.aspx";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciamos el producto nuevo con los valores de los controles
                Producto nuevo = new Producto();
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.StockActual = int.Parse(txtStock.Text);
                nuevo.ImagenUrl = txtImagenUrl.Text.Trim();
                
                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                ProductoNegocio productoNegocio = new ProductoNegocio();

                // 2. Si hay ID en la URL, es una Modificación (Update). Si no, es un Alta (Insert)
                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    productoNegocio.modificar(nuevo);
                }
                else
                {
                    productoNegocio.agregar(nuevo);
                }

                // 3. Redirección dinámica según la categoría guardada (Opción B)
                int idCategoria = nuevo.Categoria.Id;
                Response.Redirect("Productos.aspx?cat=" + idCategoria, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtImagenUrl.Text.Trim()))
            {
                imgProducto.ImageUrl = "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80";
            }
            else
            {
                imgProducto.ImageUrl = txtImagenUrl.Text.Trim();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));

                    string catSelected = ddlCategoria.SelectedValue;
                    Response.Redirect("Productos.aspx?cat=" + catSelected, false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}

