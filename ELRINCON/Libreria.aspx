<%@ Page Title="Librería" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Libreria.aspx.cs" Inherits="ELRINCON.Libreria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-custom py-5">
        <h2 class="text-dark mb-4">Categor&iacute;a: Librer&iacute;a</h2>
        
        <table class="table table-striped-columns border">
            <thead>
                <tr>
                    <th scope="col">Nombre</th>
                    <th scope="col">Marca</th>
                    <th scope="col">Stock Actual</th>
                    <th scope="col">Acción</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repProductos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# ((Dominio.Producto)Container.DataItem).Marca.Nombre %></td>
                            <td><%# Eval("StockActual") %></td>
                            <td>
                                <a href="FormularioProducto.aspx?id=<%# Eval("Id") %>&cat=1" class="btn btn-sm btn-warning">Modificar</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <div class="mt-4">
            <a href="FormularioProducto.aspx?cat=1" class="btn btn-primary me-2">Agregar Producto</a>
            <a href="Productos.aspx" class="btn btn-secondary">Volver a Productos</a>
        </div>
    </div>
</asp:Content>
