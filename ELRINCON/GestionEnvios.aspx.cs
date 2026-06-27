using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ELRINCON
{
    public partial class GestionEnvios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                CargarEnvios();
            }
        }

        private void CargarEnvios()
        {
            try
            {
                EnvioNegocio negocio = new EnvioNegocio();
                List<Envio> lista = negocio.listar();
                dgvEnvios.DataSource = lista;
                dgvEnvios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected string GetEstadoBadgeClass(string estado)
        {
            if (string.IsNullOrEmpty(estado)) return "badge bg-light text-dark border";

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

        protected void dgvEnvios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idEnvio = Convert.ToInt32(dgvEnvios.SelectedDataKey.Value);
                EnvioNegocio negocio = new EnvioNegocio();
                List<Envio> lista = negocio.listar();
                Envio seleccionado = lista.Find(x => x.Id == idEnvio);

                if (seleccionado != null)
                {
                    pnlGestion.Visible = true;
                    pnlAlertaExito.Visible = false;
                    
                    hfIdEnvio.Value = seleccionado.Id.ToString();
                    lblFacturaGestion.Text = seleccionado.NumeroFactura;
                    txtSeguimiento.Text = seleccionado.NumeroSeguimiento;
                    
                    // Buscar coincidencia en DropDownList
                    ListItem item = ddlEstado.Items.FindByValue(seleccionado.Estado);
                    if (item != null)
                    {
                        ddlEstado.SelectedValue = seleccionado.Estado;
                    }
                    else
                    {
                        ddlEstado.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Envio envio = new Envio
                {
                    Id = Convert.ToInt32(hfIdEnvio.Value),
                    NumeroSeguimiento = txtSeguimiento.Text.Trim(),
                    Estado = ddlEstado.SelectedValue
                };

                EnvioNegocio negocio = new EnvioNegocio();
                negocio.actualizar(envio);

                pnlGestion.Visible = false;
                pnlAlertaExito.Visible = true;

                // Recargar grilla
                CargarEnvios();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlGestion.Visible = false;
            pnlAlertaExito.Visible = false;
        }
    }
}
