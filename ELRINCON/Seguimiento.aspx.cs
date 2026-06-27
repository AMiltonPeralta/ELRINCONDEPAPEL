using System;
using System.Collections.Generic;
using System.Web.UI;
using Dominio;
using Negocio;

namespace ELRINCON
{
    public partial class Seguimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["n"] != null)
                {
                    string codigo = Request.QueryString["n"].ToString().Trim();
                    txtCodigoSeguimiento.Text = codigo;
                    BuscarEnvio(codigo);
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigoSeguimiento.Text.Trim();
            BuscarEnvio(codigo);
        }

        private void BuscarEnvio(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                pnlResultado.Visible = false;
                pnlError.Visible = false;
                return;
            }

            try
            {
                EnvioNegocio envioNegocio = new EnvioNegocio();
                Envio envio = envioNegocio.obtenerPorSeguimiento(codigo);

                if (envio != null)
                {
                    pnlError.Visible = false;
                    pnlResultado.Visible = true;

                    // Datos del envío
                    lblSeguimientoInfo.Text = envio.NumeroSeguimiento;
                    lblEstadoTexto.Text = envio.Estado;
                    lblDireccion.Text = envio.Direccion;
                    lblLocalidad.Text = envio.Localidad;
                    lblCodigoPostal.Text = envio.CodigoPostal;

                    // Estilo dinámico de la insignia de estado
                    if (envio.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                    {
                        lblEstadoBadge.Text = "Pendiente de Despacho";
                        lblEstadoBadge.CssClass = "badge bg-warning-subtle text-warning-emphasis border border-warning-subtle fs-6 px-3 py-2";
                    }
                    else if (envio.Estado.Equals("Despachado", StringComparison.OrdinalIgnoreCase))
                    {
                        lblEstadoBadge.Text = "Despachado / En camino";
                        lblEstadoBadge.CssClass = "badge bg-info-subtle text-info-emphasis border border-info-subtle fs-6 px-3 py-2";
                    }
                    else if (envio.Estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
                    {
                        lblEstadoBadge.Text = "Entregado";
                        lblEstadoBadge.CssClass = "badge bg-success-subtle text-success-emphasis border border-success-subtle fs-6 px-3 py-2";
                    }
                    else
                    {
                        lblEstadoBadge.Text = envio.Estado;
                        lblEstadoBadge.CssClass = "badge bg-secondary-subtle text-secondary-emphasis border border-secondary-subtle fs-6 px-3 py-2";
                    }

                    // Recuperar la venta asociada
                    VentaNegocio ventaNegocio = new VentaNegocio();
                    Venta venta = ventaNegocio.buscarPorId(envio.IdVenta);

                    if (venta != null)
                    {
                        lblFactura.Text = venta.NumeroFactura;
                        lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy HH:mm");
                        lblMetodoPago.Text = venta.Pago.Nombre;
                        lblTotal.Text = "$" + string.Format("{0:N2}", venta.Total);

                        // Cargar items
                        repItems.DataSource = venta.Detalles;
                        repItems.DataBind();
                    }
                }
                else
                {
                    pnlResultado.Visible = false;
                    pnlError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
