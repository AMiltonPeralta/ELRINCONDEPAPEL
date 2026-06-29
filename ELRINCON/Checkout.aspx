<%@ Page Title="Finalizar Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="ELRINCON.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="text-dark fw-bold mb-4">Finalizar Compra</h2>
        
        <div class="row g-4">
            <!-- Formulario de Envío y Pago -->
            <div class="col-lg-8">
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                    <h5 class="fw-bold text-dark mb-4">Datos de Entrega y Pago</h5>
                    
                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label fw-semibold text-muted small text-uppercase">Dirección de Envío</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Rivadavia 1234, Piso 2 B" />
                    </div>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtDni" class="form-label fw-semibold text-muted small text-uppercase">DNI / Documento</label>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ej: 38123456" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="txtTelefono" class="form-label fw-semibold text-muted small text-uppercase">Teléfono de Contacto</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 1123456789" />
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtLocalidad" class="form-label fw-semibold text-muted small text-uppercase">Localidad</label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: Ramos Mejía" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="txtCodigoPostal" class="form-label fw-semibold text-muted small text-uppercase">Código Postal</label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Ej: 1704" />
                        </div>
                    </div>
                    
                    <div class="mb-4">
                        <label for="ddlMetodoPago" class="form-label fw-semibold text-muted small text-uppercase">Método de Pago</label>
                        <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select" />
                    </div>
                    
                    <div class="d-flex align-items-center gap-3 pt-3 border-top">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-success px-4 py-2 fw-semibold" OnClick="btnConfirmar_Click" />
                        <a href="Carrito.aspx" class="text-secondary fw-semibold text-decoration-none">Volver al Carrito</a>
                    </div>
                </div>
            </div>
            
            <!-- Resumen simplificado -->
            <div class="col-lg-4">
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                    <h5 class="fw-bold text-dark mb-4">Resumen del Pedido</h5>
                    
                    <div class="mb-4">
                        <asp:Repeater ID="repDetallePedido" runat="server">
                            <ItemTemplate>
                                <div class="d-flex justify-content-between align-items-center mb-2 pb-2 border-bottom border-light-subtle" style="font-size: 0.9rem;">
                                    <div class="d-flex align-items-center gap-2">
                                        <img src='<%# string.IsNullOrEmpty(Eval("Producto.ImagenUrl") as string) ? "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=100&q=80" : Eval("Producto.ImagenUrl") %>' 
                                             class="rounded bg-light border p-1" style="width: 36px; height: 36px; object-fit: contain;" />
                                        <div>
                                            <span class="d-block fw-bold text-dark text-truncate" style="max-width: 140px;" title='<%# Eval("Producto.Nombre") %>'><%# Eval("Producto.Nombre") %></span>
                                            <span class="text-muted small" style="font-size: 0.75rem;">Cant: <%# Eval("Cantidad") %></span>
                                        </div>
                                    </div>
                                    <span class="fw-bold text-dark">$<%# string.Format("{0:N2}", Eval("Subtotal")) %></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    
                    <div class="d-flex justify-content-between mb-3">
                        <span class="text-muted">Total de Artículos</span>
                        <span class="text-dark fw-bold"><asp:Label ID="lblCantidadItems" runat="server" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-3">
                        <span class="text-muted">Subtotal</span>
                        <span class="text-dark fw-semibold"><asp:Label ID="lblSubtotal" runat="server" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-3">
                        <span class="text-muted">Envío</span>
                        <span class="text-success fw-semibold">Gratis</span>
                    </div>
                    <hr class="my-3 border-light-subtle" />
                    <div class="d-flex justify-content-between align-items-center">
                        <span class="text-dark fw-bold fs-5">Total a Pagar</span>
                        <span class="text-primary fw-bold fs-4"><asp:Label ID="lblTotal" runat="server" /></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
