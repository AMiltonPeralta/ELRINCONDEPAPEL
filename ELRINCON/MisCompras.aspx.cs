using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ELRINCON
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar que el usuario esté logueado
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                VentaNegocio negocio = new VentaNegocio();
                List<Venta> lista = negocio.listarPorUsuario(usuario.Id);

                if (lista == null || lista.Count == 0)
                {
                    pnlVacio.Visible = true;
                    dgvCompras.Visible = false;
                }
                else
                {
                    pnlVacio.Visible = false;
                    dgvCompras.Visible = true;
                    dgvCompras.DataSource = lista;
                    dgvCompras.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected string GetEstadoBadgeClass(object estadoObj)
        {
            if (estadoObj == null || estadoObj == DBNull.Value) 
                return "badge bg-secondary-subtle text-secondary-emphasis border border-secondary-subtle";

            string estado = estadoObj.ToString();
            switch (estado.ToLower())
            {
                case "pendiente":
                    return "badge bg-warning-subtle text-warning-emphasis border border-warning-subtle";
                case "en preparación":
                case "en preparacion":
                    return "badge bg-secondary bg-opacity-10 text-secondary border border-secondary border-opacity-20";
                case "despachado":
                    return "badge bg-info-subtle text-info-emphasis border border-info-subtle";
                case "entregado":
                    return "badge bg-success-subtle text-success-emphasis border border-success-subtle";
                default:
                    return "badge bg-light text-dark border";
            }
        }

        protected void dgvCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idVenta = Convert.ToInt32(dgvCompras.SelectedDataKey.Value);
                VentaNegocio negocio = new VentaNegocio();
                Venta seleccionada = negocio.buscarPorId(idVenta);

                if (seleccionada != null)
                {
                    pnlDetalle.Visible = true;
                    lblFacturaDetalle.Text = seleccionada.NumeroFactura;
                    lblTotalDetalle.Text = "$" + string.Format("{0:N2}", seleccionada.Total);
                    
                    repItems.DataSource = seleccionada.Detalles;
                    repItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            pnlDetalle.Visible = false;
        }
    }
}
