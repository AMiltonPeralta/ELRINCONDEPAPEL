<%@ Page Title="Seguimiento de Envío" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="ELRINCON.Seguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <!-- Título Principal -->
        <h2 class="text-dark fw-bold mb-4">Seguimiento de Envíos</h2>

        <!-- Buscador -->
        <div class="card shadow-sm border border-light-subtle rounded-3 p-4 mb-4 bg-white">
            <h5 class="fw-bold text-dark mb-3">Consultar Estado de Envío</h5>
            <p class="text-muted small">Ingresa el código único de seguimiento de tu pedido (ej. ENV-123456) para conocer su estado actual.</p>
            <div class="row g-3 align-items-center">
                <div class="col-md-8">
                    <div class="input-group">
                        <span class="input-group-text bg-light text-muted">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-box-seam" viewBox="0 0 16 16">
                              <path d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.962L10.404 2zm3.564 1.426L5.596 5 8 5.961 14.154 3.5zm.328 1.175L6 6.143v5.807l8-3.2V3.714zM5 11.95V6.143L1 4.543v5.2l4 2.207zM1.846 12.5l6.154 2.5 6.154-2.5-6.154-2.46zM15 13a1 1 0 1 1-2 0 1 1 0 0 1 2 0m-2 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0M1.5 4H2v8.5a.5.5 0 0 1-.5.5h-.032L1 12.5V4.5A1.5 1.5 0 0 1 1.5 4M14.5 4h.032l-.468.5v8H14a.5.5 0 0 1-.5-.5V4.5a1.5 1.5 0 0 1 1-1.5"/>
                            </svg>
                        </span>
                        <asp:TextBox ID="txtCodigoSeguimiento" runat="server" CssClass="form-control" placeholder="Ej: ENV-123456" MaxLength="50" autocomplete="off" />
                    </div>
                </div>
                <div class="col-md-4 d-grid">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Envío" CssClass="btn btn-primary fw-semibold py-2" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <!-- Panel de Alerta/Error -->
        <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger shadow-sm d-flex align-items-center gap-2 border border-danger-subtle rounded-3" Visible="false">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-exclamation-triangle-fill text-danger flex-shrink-0" viewBox="0 0 16 16">
              <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
            </svg>
            <div>
                <asp:Label ID="lblMensajeError" runat="server" Text="No se encontró ningún envío asociado al número de seguimiento ingresado. Por favor, verifique el código e intente nuevamente." />
            </div>
        </asp:Panel>

        <!-- Panel de Resultado -->
        <asp:Panel ID="pnlResultado" runat="server" CssClass="row g-4" Visible="false">
            
            <!-- Detalle del Envío y Estado -->
            <div class="col-lg-8">
                <!-- Tarjeta de Estado del Envío -->
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 mb-4 bg-white">
                    <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
                        <h5 class="fw-bold text-dark mb-0">Detalles del Envío</h5>
                        <asp:Label ID="lblEstadoBadge" runat="server" CssClass="badge fs-6 px-3 py-2 border rounded" />
                    </div>

                    <div class="row g-3">
                        <div class="col-sm-6">
                            <span class="text-muted small d-block">Número de Seguimiento</span>
                            <span class="text-dark fw-bold fs-5"><asp:Label ID="lblSeguimientoInfo" runat="server" /></span>
                        </div>
                        <div class="col-sm-6">
                            <span class="text-muted small d-block">Estado Actual</span>
                            <span class="text-dark fw-semibold"><asp:Label ID="lblEstadoTexto" runat="server" /></span>
                        </div>
                        <hr class="my-3 border-light-subtle" />
                        <div class="col-12">
                            <h6 class="fw-bold text-dark mb-2">Dirección de Entrega</h6>
                            <p class="mb-0 text-secondary">
                                <strong>Calle/Dirección:</strong> <asp:Label ID="lblDireccion" runat="server" /><br />
                                <strong>Localidad:</strong> <asp:Label ID="lblLocalidad" runat="server" /><br />
                                <strong>Código Postal (CP):</strong> <asp:Label ID="lblCodigoPostal" runat="server" />
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Detalle de Productos Comprados -->
                <div class="card shadow-sm border border-light-subtle rounded-3 overflow-hidden bg-white">
                    <div class="px-4 py-3 bg-light border-bottom">
                        <h6 class="fw-bold text-dark mb-0">Productos en este Pedido</h6>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-align-middle mb-0">
                            <thead class="table-light text-secondary small text-uppercase">
                                <tr>
                                    <th scope="col" class="ps-4">Producto</th>
                                    <th scope="col">Precio</th>
                                    <th scope="col" class="text-center" style="width: 100px;">Cantidad</th>
                                    <th scope="col" class="pe-4 text-end">Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repItems" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="ps-4 py-3">
                                                <div class="d-flex align-items-center gap-3">
                                                    <img src='<%# string.IsNullOrEmpty(Eval("Articulo.ImagenUrl") as string) ? "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=100&q=80" : Eval("Articulo.ImagenUrl") %>' 
                                                         alt='<%# Eval("Articulo.Nombre") %>' 
                                                         class="rounded border object-fit-cover" 
                                                         style="width: 45px; height: 45px; min-width: 45px;" />
                                                    <div class="min-w-0">
                                                        <h6 class="mb-0 fw-semibold text-dark text-truncate" style="font-size: 0.9rem;" title='<%# Eval("Articulo.Nombre") %>'>
                                                            <%# Eval("Articulo.Nombre") %>
                                                        </h6>
                                                    </div>
                                                </div>
                                            </td>
                                            <td class="py-3">
                                                <span class="text-dark">$<%# string.Format("{0:N2}", Eval("PrecioUnitario")) %></span>
                                            </td>
                                            <td class="py-3 text-center">
                                                <span class="fw-bold text-dark"><%# Eval("Cantidad") %></span>
                                            </td>
                                            <td class="py-3 pe-4 text-end">
                                                <span class="text-primary fw-bold">$<%# string.Format("{0:N2}", Eval("Subtotal")) %></span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- Resumen de Compra -->
            <div class="col-lg-4">
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                    <h5 class="fw-bold text-dark mb-4">Información del Pedido</h5>

                    <div class="d-flex justify-content-between mb-3 border-bottom pb-2 border-light-subtle">
                        <span class="text-muted small">N° Factura</span>
                        <span class="text-dark fw-bold"><asp:Label ID="lblFactura" runat="server" /></span>
                    </div>

                    <div class="d-flex justify-content-between mb-3 border-bottom pb-2 border-light-subtle">
                        <span class="text-muted small">Fecha de Compra</span>
                        <span class="text-dark fw-semibold"><asp:Label ID="lblFecha" runat="server" /></span>
                    </div>

                    <div class="d-flex justify-content-between mb-3 border-bottom pb-2 border-light-subtle">
                        <span class="text-muted small">Método de Pago</span>
                        <span class="text-dark fw-semibold"><asp:Label ID="lblMetodoPago" runat="server" /></span>
                    </div>

                    <div class="d-flex justify-content-between align-items-center mt-4">
                        <span class="text-dark fw-bold fs-5">Monto Total</span>
                        <span class="text-primary fw-bold fs-4"><asp:Label ID="lblTotal" runat="server" /></span>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>

    <style>
        .table-align-middle td, .table-align-middle th {
            vertical-align: middle;
        }
        .object-fit-cover {
            object-fit: cover;
        }
    </style>
</asp:Content>
