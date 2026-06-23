<%@ Page Title="Nuevo Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="ELRINCON.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

                <div class="mb-4">
                    <label for="txtImagenUrl" class="form-label fw-semibold text-secondary">URL de la Imagen</label>
                    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.jpg" MaxLength="500" />
                </div>
                
                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-4">
                    <asp:HyperLink ID="lnkCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4 me-md-2" />
                    <asp:Button ID="btnAceptar" runat="server" Text="Guardar Producto" CssClass="btn btn-primary px-4 fw-semibold" OnClick="btnAceptar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

