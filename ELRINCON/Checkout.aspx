<%@ Page Title="Finalizar Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="ELRINCON.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container py-4">
        <h2 class="text-dark fw-bold mb-4">Finalizar Compra</h2>
        
        <div class="row g-4">
            <!-- Formulario de Envío y Pago -->
            <div class="col-lg-8">
                <asp:UpdatePanel ID="updEntregaPago" runat="server">
                    <ContentTemplate>
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
                                <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged" />
                            </div>

                            <!-- Panel para Tarjetas -->
                            <asp:Panel ID="pnlTarjeta" runat="server" Visible="false" CssClass="card p-3 mb-4 bg-light border-light-subtle">
                                <h6 class="fw-bold text-dark mb-3">Detalle de la Tarjeta</h6>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="ddlBanco" class="form-label fw-semibold text-muted small text-uppercase">Banco Emisor / Empresa</label>
                                        <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Banco Galicia" Value="Galicia" />
                                            <asp:ListItem Text="Banco Santander" Value="Santander" />
                                            <asp:ListItem Text="Banco BBVA" Value="BBVA" />
                                            <asp:ListItem Text="Otro" Value="Otro" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="txtTarjetaNumero" class="form-label fw-semibold text-muted small text-uppercase">Número de Tarjeta</label>
                                        <asp:TextBox ID="txtTarjetaNumero" runat="server" CssClass="form-control" placeholder="XXXX XXXX XXXX XXXX" oninput="formatCardNumber(this)" MaxLength="19" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label for="txtTarjetaVence" class="form-label fw-semibold text-muted small text-uppercase">Fecha de Vencimiento</label>
                                        <asp:TextBox ID="txtTarjetaVence" runat="server" CssClass="form-control" placeholder="MM/AA" MaxLength="5" oninput="formatExpiry(this)" />
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="txtTarjetaCVV" class="form-label fw-semibold text-muted small text-uppercase">Código de Seguridad (CVV)</label>
                                        <asp:TextBox ID="txtTarjetaCVV" runat="server" CssClass="form-control" placeholder="123" MaxLength="3" oninput="this.value = this.value.replace(/\D/g, '').substring(0, 3)" />
                                    </div>
                                </div>
                            </asp:Panel>

                            <!-- Panel para Pago Fácil -->
                            <asp:Panel ID="pnlPagoFacil" runat="server" Visible="false" CssClass="alert alert-info border-info-subtle mb-4 shadow-sm">
                                <div class="d-flex gap-2 align-items-center">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-info-circle-fill text-info flex-shrink-0" viewBox="0 0 16 16">
                                        <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16m.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2"/>
                                    </svg>
                                    <div>
                                        Con el número de factura que recibirás al confirmar, podrás realizar el pago en cualquier sucursal de Pago Fácil.
                                    </div>
                                </div>
                            </asp:Panel>
                            
                            <div class="d-flex align-items-center gap-3 pt-3 border-top">
                                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-success px-4 py-2 fw-semibold" OnClick="btnConfirmar_Click" />
                                <a href="Carrito.aspx" class="text-secondary fw-semibold text-decoration-none">Volver al Carrito</a>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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

    <!-- Scripts de formateo e interactividad del pago -->
    <script>
        function formatCardNumber(input) {
            var val = input.value.replace(/\D/g, '');
            val = val.substring(0, 16);
            var parts = [];
            for (var i = 0; i < val.length; i += 4) {
                parts.push(val.substring(i, i + 4));
            }
            input.value = parts.join(' ');
        }

        function formatExpiry(input) {
            var val = input.value.replace(/\D/g, '');
            if (val.length >= 2) {
                input.value = val.substring(0, 2) + '/' + val.substring(2, 4);
            } else {
                input.value = val;
            }
        }
    </script>
</asp:Content>
