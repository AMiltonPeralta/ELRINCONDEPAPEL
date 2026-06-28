<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="ELRINCON.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="text-dark fw-bold mb-4">Mis Compras</h2>

        <!-- Grilla de Compras -->
        <div class="card shadow-sm border border-light-subtle rounded-3 overflow-hidden bg-white p-4 mb-4">
            <h5 class="fw-bold text-dark mb-4">Historial de Pedidos</h5>
            
            <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center py-5 border rounded bg-light">
                <p class="text-muted fs-5 mb-0">Aún no has realizado ninguna compra en nuestra tienda.</p>
                <a href="Productos.aspx" class="btn btn-primary mt-3">Ver catálogo de productos</a>
            </asp:Panel>

            <div class="table-responsive">
                <asp:GridView ID="dgvCompras" runat="server" AutoGenerateColumns="false" 
                    CssClass="table table-hover align-middle mb-0" GridLines="None"
                    DataKeyNames="Id" OnSelectedIndexChanged="dgvCompras_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="NumeroFactura" HeaderText="Factura" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" />
                        <asp:TemplateField HeaderText="Método de Pago" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <%# Eval("Pago.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <span class="fw-bold text-dark">$<%# string.Format("{0:N2}", Eval("Total")) %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Envío" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <span class='<%# GetEstadoBadgeClass(Eval("DatosEnvio.Estado") as string) %>'>
                                    <%# Eval("DatosEnvio") == null ? "Sin Envío" : Eval("DatosEnvio.Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seguimiento" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkSeguimiento" runat="server" 
                                    NavigateUrl='<%# "Seguimiento.aspx?n=" + Eval("DatosEnvio.NumeroSeguimiento") %>'
                                    Text='<%# Eval("DatosEnvio.NumeroSeguimiento") %>'
                                    Visible='<%# Eval("DatosEnvio") != null && !string.IsNullOrEmpty(Eval("DatosEnvio.NumeroSeguimiento") as string) %>'
                                    CssClass="fw-semibold text-primary" ToolTip="Ver detalle de entrega" />
                                <span class="text-muted small" runat="server" 
                                    visible='<%# Eval("DatosEnvio") == null || string.IsNullOrEmpty(Eval("DatosEnvio.NumeroSeguimiento") as string) %>'>
                                    No asignado
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" SelectText="Ver Detalle" 
                            HeaderStyle-CssClass="table-light text-secondary small text-uppercase text-end"
                            ItemStyle-CssClass="text-end"
                            ControlStyle-CssClass="btn btn-sm btn-outline-primary fw-semibold px-3 py-1" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Panel de Detalle del Pedido Seleccionado -->
        <asp:Panel ID="pnlDetalle" runat="server" Visible="false" CssClass="card shadow-sm border border-light-subtle rounded-3 overflow-hidden bg-white mx-auto" style="max-width: 800px;">
            <div class="px-4 py-3 bg-light border-bottom d-flex justify-content-between align-items-center">
                <h6 class="fw-bold text-dark mb-0">Detalle de Productos en Factura: <span class="text-primary"><asp:Label ID="lblFacturaDetalle" runat="server" /></span></h6>
                <asp:Button ID="btnCerrarDetalle" runat="server" Text="Cerrar" CssClass="btn btn-sm btn-outline-secondary px-2 py-0.5" OnClick="btnCerrarDetalle_Click" />
            </div>
            
            <div class="table-responsive">
                <table class="table table-align-middle mb-0">
                    <thead class="table-light text-secondary small text-uppercase">
                        <tr>
                            <th scope="col" class="ps-4">Producto</th>
                            <th scope="col">Precio Unitario</th>
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
                                                 style="width: 40px; height: 40px; min-width: 40px;" />
                                            <div>
                                                <h6 class="mb-0 fw-semibold text-dark text-truncate" style="font-size: 0.85rem;" title='<%# Eval("Articulo.Nombre") %>'>
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
                                    <td class="py-3 pe-4 text-end font-monospace">
                                        <span class="text-primary fw-bold">$<%# string.Format("{0:N2}", Eval("Subtotal")) %></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            
            <div class="card-footer bg-light px-4 py-3 d-flex justify-content-between align-items-center border-top">
                <span class="text-muted fw-semibold">Monto Total del Pedido:</span>
                <span class="text-primary fw-bold fs-5"><asp:Label ID="lblTotalDetalle" runat="server" /></span>
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
