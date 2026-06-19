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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
                        ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
                        ddlCategoria.Enabled = false; // Deshabilitar cambio de categoría en edición

                        // Configurar enlace cancelar dinámico
                        string catSelected = seleccionado.Categoria.Id.ToString();
                        if (catSelected == "1") lnkCancelar.NavigateUrl = "Libreria.aspx";
                        else if (catSelected == "2") lnkCancelar.NavigateUrl = "Computacion.aspx";
                        else if (catSelected == "3") lnkCancelar.NavigateUrl = "Gaming.aspx";
                        else if (catSelected == "4") lnkCancelar.NavigateUrl = "JuegosMesa.aspx";
                        else lnkCancelar.NavigateUrl = "Productos.aspx";
                    }
                    else if (!string.IsNullOrEmpty(catId))
                    {
                        // Autoseleccionar la categoría de origen en Alta
                        ddlCategoria.SelectedValue = catId;
                        ddlCategoria.Enabled = false;

                        // Configurar el enlace de Cancelar dinámicamente según la categoría de origen
                        if (catId == "1") lnkCancelar.NavigateUrl = "Libreria.aspx";
                        else if (catId == "2") lnkCancelar.NavigateUrl = "Computacion.aspx";
                        else if (catId == "3") lnkCancelar.NavigateUrl = "Gaming.aspx";
                        else if (catId == "4") lnkCancelar.NavigateUrl = "JuegosMesa.aspx";
                        else lnkCancelar.NavigateUrl = "Productos.aspx";
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
                if (idCategoria == 1) Response.Redirect("Libreria.aspx", false);
                else if (idCategoria == 2) Response.Redirect("Computacion.aspx", false);
                else if (idCategoria == 3) Response.Redirect("Gaming.aspx", false);
                else if (idCategoria == 4) Response.Redirect("JuegosMesa.aspx", false);
                else Response.Redirect("Productos.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
