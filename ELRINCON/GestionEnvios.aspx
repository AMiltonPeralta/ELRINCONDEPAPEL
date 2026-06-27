<%@ Page Title="Gestión de Envíos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionEnvios.aspx.cs" Inherits="ELRINCON.GestionEnvios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="text-dark fw-bold mb-4">Administración de Envíos</h2>

        <!-- Alert de Éxito -->
        <asp:Panel ID="pnlAlertaExito" runat="server" CssClass="alert alert-success shadow-sm d-flex align-items-center gap-2 border border-success-subtle rounded-3 mb-4" Visible="false">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-check-circle-fill text-success flex-shrink-0" viewBox="0 0 16 16">
              <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
            </svg>
            <div>
                El envío se ha actualizado correctamente.
            </div>
        </asp:Panel>

        <!-- Tabla/GridView de Envíos -->
        <div class="card shadow-sm border border-light-subtle rounded-3 overflow-hidden bg-white p-4">
            <h5 class="fw-bold text-dark mb-4">Listado de Pedidos y Envíos</h5>
            <div class="table-responsive">
                <asp:GridView ID="dgvEnvios" runat="server" AutoGenerateColumns="false" 
                    CssClass="table table-hover align-middle mb-0" GridLines="None"
                    DataKeyNames="Id" OnSelectedIndexChanged="dgvEnvios_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="NumeroFactura" HeaderText="Factura" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" />
                        <asp:BoundField DataField="FechaVenta" HeaderText="Fecha Compra" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" />
                        <asp:BoundField DataField="Localidad" HeaderText="Localidad" HeaderStyle-CssClass="table-light text-secondary small text-uppercase" />
                        <asp:TemplateField HeaderText="Seguimiento" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <span class="fw-semibold text-primary"><%# string.IsNullOrEmpty(Eval("NumeroSeguimiento") as string) ? "No asignado" : Eval("NumeroSeguimiento") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="table-light text-secondary small text-uppercase">
                            <ItemTemplate>
                                <span class='<%# GetEstadoBadgeClass(Eval("Estado") as string) %>'>
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" SelectText="Gestionar" 
                            HeaderStyle-CssClass="table-light text-secondary small text-uppercase text-end"
                            ItemStyle-CssClass="text-end"
                            ControlStyle-CssClass="btn btn-sm btn-outline-primary fw-semibold px-3 py-1" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Panel de Gestión/Edición -->
        <asp:Panel ID="pnlGestion" runat="server" Visible="false" CssClass="card shadow-sm border border-light-subtle rounded-3 p-4 mt-4 bg-white mx-auto" style="max-width: 600px;">
            <div class="border-bottom pb-3 mb-4">
                <h5 class="fw-bold text-dark mb-1">Gestionar Envío</h5>
                <p class="text-muted small mb-0">Factura: <strong class="text-dark"><asp:Label ID="lblFacturaGestion" runat="server" /></strong></p>
            </div>

            <asp:HiddenField ID="hfIdEnvio" runat="server" />

            <div class="mb-3">
                <label for="txtSeguimiento" class="form-label fw-semibold">Número de Seguimiento</label>
                <asp:TextBox ID="txtSeguimiento" runat="server" CssClass="form-control" placeholder="Ej: ENV-123456" MaxLength="50" />
                <p class="text-muted small mt-1">Código único de seguimiento para que el cliente pueda consultar el estado.</p>
            </div>

            <div class="mb-4">
                <label for="ddlEstado" class="form-label fw-semibold">Estado del Envío</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem Value="Pendiente" Text="Pendiente (Creado)" />
                    <asp:ListItem Value="En preparación" Text="En preparación (Embalaje)" />
                    <asp:ListItem Value="Despachado" Text="Despachado (En camino)" />
                    <asp:ListItem Value="Entregado" Text="Entregado (Completado)" />
                </asp:DropDownList>
            </div>

            <div class="d-flex gap-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success fw-semibold flex-fill py-2" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary py-2 px-4" OnClick="btnCancelar_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
