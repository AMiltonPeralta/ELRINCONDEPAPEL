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
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Validar que el carrito no esté vacío
                if (Session["Carrito"] == null || ((List<ItemCarrito>)Session["Carrito"]).Count == 0)
                {
                    Response.Redirect("Carrito.aspx", false);
                    return;
                }

                List<ItemCarrito> carrito = (List<ItemCarrito>)Session["Carrito"];
                int totalItems = carrito.Sum(x => x.Cantidad);
                decimal totalPagar = carrito.Sum(x => x.Subtotal);

                lblCantidadItems.Text = totalItems.ToString();
                lblSubtotal.Text = "$" + string.Format("{0:N2}", totalPagar);
                lblTotal.Text = "$" + string.Format("{0:N2}", totalPagar);

                if (!IsPostBack)
                {
                    CargarMetodosPago();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private void CargarMetodosPago()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdMetodoPago, Nombre FROM MetodosPago WHERE Activo = 1");
                datos.ejecutarLectura();
                ddlMetodoPago.DataSource = datos.Lector;
                ddlMetodoPago.DataValueField = "IdMetodoPago";
                ddlMetodoPago.DataTextField = "Nombre";
                ddlMetodoPago.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validación Personalizada en C# usando la clase Validacion de la capa de Negocio
                if (Validacion.validaTextoVacio(txtDireccion) || 
                    Validacion.validaTextoVacio(txtLocalidad) || 
                    Validacion.validaTextoVacio(txtCodigoPostal))
                {
                    Session.Add("error", "Debes completar todos los campos del formulario de envío para poder continuar.");
                    Response.Redirect("Error.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Obtener datos del carrito de la sesión
                List<ItemCarrito> carrito = (List<ItemCarrito>)Session["Carrito"];
                decimal totalPagar = carrito.Sum(x => x.Subtotal);

                // Armar el objeto Venta
                Venta nuevaVenta = new Venta();
                nuevaVenta.NumeroFactura = "FAC-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (Session["usuario"] != null)
                {
                    nuevaVenta.Cliente = (Usuario)Session["usuario"];
                }
                else
                {
                    nuevaVenta.Cliente = new Usuario { Id = 2 }; // Mocking Milton Cliente
                }
                nuevaVenta.Fecha = DateTime.Now;
                nuevaVenta.Total = totalPagar;
                nuevaVenta.Pago = new MetodoPago 
                { 
                    Id = int.Parse(ddlMetodoPago.SelectedValue), 
                    Nombre = ddlMetodoPago.SelectedItem.Text 
                };

                // Armar Envio
                Envio nuevoEnvio = new Envio();
                nuevoEnvio.Direccion = txtDireccion.Text.Trim();
                nuevoEnvio.Localidad = txtLocalidad.Text.Trim();
                nuevoEnvio.CodigoPostal = txtCodigoPostal.Text.Trim();
                nuevoEnvio.NumeroSeguimiento = "ENV-" + new Random().Next(100000, 999999).ToString();
                nuevoEnvio.Estado = "Pendiente";
                
                nuevaVenta.DatosEnvio = nuevoEnvio;

                // Armar Detalles
                foreach (var item in carrito)
                {
                    DetalleVenta det = new DetalleVenta();
                    det.Articulo = item.Producto;
                    det.Cantidad = item.Cantidad;
                    det.PrecioUnitario = item.PrecioUnitario;
                    nuevaVenta.Detalles.Add(det);
                }

                // Guardar en Base de Datos
                VentaNegocio negocio = new VentaNegocio();
                negocio.agregar(nuevaVenta);

                // Enviar Correo de Confirmación
                try
                {
                    EmailService emailService = new EmailService();
                    string cuerpo = $@"
                        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                            <h2 style='color: #0d6efd;'>¡Gracias por tu compra en El Rincón del Papel!</h2>
                            <p>Hola {nuevaVenta.Cliente.Nombre},</p>
                            <p>Tu pedido ha sido procesado con éxito. A continuación te detallamos los datos de tu compra:</p>
                            
                            <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                                <thead>
                                    <tr style='background-color: #f8f9fa;'>
                                        <th style='padding: 10px; border: 1px solid #dee2e6; text-align: left;'>Factura</th>
                                        <th style='padding: 10px; border: 1px solid #dee2e6; text-align: left;'>Método de Pago</th>
                                        <th style='padding: 10px; border: 1px solid #dee2e6; text-align: right;'>Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style='padding: 10px; border: 1px solid #dee2e6;'>{nuevaVenta.NumeroFactura}</td>
                                        <td style='padding: 10px; border: 1px solid #dee2e6;'>{nuevaVenta.Pago.Nombre}</td>
                                        <td style='padding: 10px; border: 1px solid #dee2e6; text-align: right; font-weight: bold; color: #0d6efd;'>${string.Format("{0:N2}", totalPagar)}</td>
                                    </tr>
                                </tbody>
                            </table>
                            
                            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 6px; margin: 20px 0; border-left: 4px solid #198754;'>
                                <h4 style='margin-top: 0; color: #198754;'>Información de Envío</h4>
                                <p style='margin: 5px 0;'><strong>Dirección:</strong> {nuevoEnvio.Direccion}, {nuevoEnvio.Localidad} (CP: {nuevoEnvio.CodigoPostal})</p>
                                <p style='margin: 5px 0;'><strong>Número de Seguimiento:</strong> <span style='font-size: 1.1em; font-weight: bold; color: #0d6efd;'>{nuevoEnvio.NumeroSeguimiento}</span></p>
                            </div>
                            
                            <p style='color: #6c757d; font-size: 0.9em;'>Si tienes alguna duda con tu envío, puedes contactarnos respondiendo a este correo.</p>
                            <p style='margin-top: 30px; border-top: 1px solid #e0e0e0; padding-top: 15px; font-weight: bold; color: #0d6efd;'>El Rincón del Papel</p>
                        </div>";

                    emailService.armarCorreo(string.IsNullOrEmpty(nuevaVenta.Cliente.Email) ? "cliente@elrincon.com" : nuevaVenta.Cliente.Email, "Confirmación de Compra - El Rincón del Papel", cuerpo);
                    emailService.enviarEmail();
                }
                catch (Exception)
                {
                    // No interrumpir el flujo del usuario si el envío de correo falla
                }

                // Vaciar Carrito
                Session["Carrito"] = null;

                // Redirigir a Confirmacion
                Response.Redirect("Confirmacion.aspx?seguimiento=" + nuevoEnvio.NumeroSeguimiento, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Capturar la excepción esperada de la redirección
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
