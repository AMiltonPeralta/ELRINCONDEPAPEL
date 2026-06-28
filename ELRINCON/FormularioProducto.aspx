<%@ Page Title="Nuevo Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="ELRINCON.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="row justify-content-center">
        <div class="col-md-6 col-lg-5">
            <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white mt-4">
                <div class="text-center mb-4">
                    <h2 class="fw-bold text-dark mb-1">Formulario de Producto</h2>
                    <p class="text-muted small">Carga y edición de productos</p>
                </div>

                <div class="mb-3">
                    <label for="txtId" class="form-label fw-semibold text-secondary">ID</label>
                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
                
                <div class="mb-3">
                    <label for="txtNombre" class="form-label fw-semibold text-secondary">Nombre del Producto</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Regla de plástico 30cm" Required="true" MaxLength="100" />
                </div>
                
                <div class="mb-3">
                    <label for="ddlMarca" class="form-label fw-semibold text-secondary">Marca</label>
                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label for="ddlCategoria" class="form-label fw-semibold text-secondary">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                </div>
                
                <div class="mb-3">
                    <label for="txtStock" class="form-label fw-semibold text-secondary">Stock Inicial</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" min="0" placeholder="0" Required="true" />
                </div>

                <div class="mb-3">
                    <label for="txtImagenUrl" class="form-label fw-semibold text-secondary">URL de la Imagen</label>
                    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.jpg" AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                </div>
                
                <div class="mb-4 text-center">
                    <asp:Image ID="imgProducto" runat="server" CssClass="img-fluid rounded border shadow-sm" style="max-height: 180px; min-height: 120px; min-width: 120px; object-fit: cover;" onerror="this.src='https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80';" />
                </div>
                
                <asp:UpdatePanel ID="UpdatePanelDelete" runat="server">
                    <ContentTemplate>
                        <div class="d-flex justify-content-between align-items-center mt-4">
                            <div>
                                <% if (Request.QueryString["id"] != null) { %>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger fw-semibold" OnClick="btnEliminar_Click" />
                                <% } %>
                            </div>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                <asp:HyperLink ID="lnkCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
                                <asp:Button ID="btnAceptar" runat="server" Text="Guardar Producto" CssClass="btn btn-primary px-4 fw-semibold" OnClick="btnAceptar_Click" />
                            </div>
                        </div>

                        <% if (ConfirmaEliminacion) { %>
                            <div class="alert alert-danger border border-danger-subtle rounded-3 p-3 mt-3 d-flex flex-column flex-sm-row align-items-sm-center justify-content-between gap-3">
                                <div class="form-check d-flex align-items-center m-0">
                                    <input type="checkbox" id="chkConfirmaEliminacion" runat="server" class="form-check-input flex-shrink-0" style="width: 1.25rem; height: 1.25rem; cursor: pointer;" />
                                    <label class="form-check-label fw-semibold text-danger-emphasis ms-2 lh-1" for="MainContent_chkConfirmaEliminacion" style="cursor: pointer; user-select: none;">
                                        Confirmar eliminación definitiva
                                    </label>
                                </div>
                                <asp:Button ID="btnConfirmaEliminar" runat="server" Text="Eliminar de verdad" CssClass="btn btn-danger btn-sm fw-semibold px-3 py-1.5" OnClick="btnConfirmaEliminar_Click" />
                            </div>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

