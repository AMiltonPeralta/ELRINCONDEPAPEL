<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="ELRINCON.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-custom text-center py-5">
        <h2 class="text-dark mb-4">Productos</h2>
        
        <div class="mb-4 text-start mx-auto" style="max-width: 320px;">
            <label for="ddlCategorias" class="form-label fw-bold">Categor&iacute;as</label>
            <asp:DropDownList ID="ddlCategorias" CssClass="form-select mb-3" runat="server">
                <asp:ListItem Text="Seleccione una categor&iacute;a" Value="0" />
                <asp:ListItem Text="Librer&iacute;a" Value="1" />
                <asp:ListItem Text="Computaci&oacute;n" Value="2" />
                <asp:ListItem Text="Gaming" Value="3" />
                <asp:ListItem Text="Juegos de mesa" Value="4" />
            </asp:DropDownList>
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary w-100" OnClick="btnAceptar_Click" />
        </div>

        <a href="Default.aspx" class="btn btn-secondary">Volver al Inicio</a>
    </div>
</asp:Content>

