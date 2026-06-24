<%@ Page Title="Carrito de Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="ELRINCON.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="text-dark fw-bold mb-4">Carrito de Compras</h2>
        
        <!-- Panel 1: Carrito Vacío -->
        <asp:Panel ID="pnlCarritoVacio" runat="server" CssClass="text-center py-5 border rounded bg-white shadow-sm" Visible="false">
            <div class="py-4">
                <svg xmlns="http://www.w3.org/2000/svg" width="64" height="64" fill="currentColor" class="bi bi-cart-x text-muted mb-3" viewBox="0 0 16 16">
                  <path d="M7.354 5.646a.5.5 0 1 0-.708.708L7.793 7.5 6.646 8.646a.5.5 0 1 0 .708.708L8.5 8.207l1.146 1.147a.5.5 0 0 0 .708-.708L9.207 7.5l1.147-1.146a.5.5 0 0 0-.708-.708L8.5 6.793z"/>
                  <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
                </svg>
                <h4 class="text-secondary fw-semibold">Tu carrito está vacío</h4>
                <p class="text-muted mb-4">¿Aún no te decidiste? Explora nuestro catálogo y encontrá lo que buscás.</p>
                <a href="Productos.aspx" class="btn btn-primary px-4 fw-semibold">Ver Productos</a>
            </div>
        </asp:Panel>

        <!-- Panel 2: Listado del Carrito -->
        <asp:Panel ID="pnlCarritoConItems" runat="server" CssClass="row g-4" Visible="false">
            <!-- Tabla de items -->
            <div class="col-lg-8">
                <div class="card shadow-sm border border-light-subtle rounded-3 overflow-hidden">
                    <div class="table-responsive">
                        <table class="table table-align-middle mb-0">
                            <thead class="table-light text-secondary small text-uppercase">
                                <tr>
                                    <th scope="col" class="ps-4">Producto</th>
                                    <th scope="col">Precio</th>
                                    <th scope="col" class="text-center" style="width: 140px;">Cantidad</th>
                                    <th scope="col">Subtotal</th>
                                    <th scope="col" class="pe-4 text-end"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repItems" runat="server" OnItemCommand="repItems_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <!-- Imagen y Nombre -->
                                            <td class="ps-4 py-3">
                                                <div class="d-flex align-items-center gap-3">
                                                    <img src='<%# string.IsNullOrEmpty(Eval("Producto.ImagenUrl") as string) ? "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=100&q=80" : Eval("Producto.ImagenUrl") %>' 
                                                         alt='<%# Eval("Producto.Nombre") %>' 
                                                         class="rounded border object-fit-cover" 
                                                         style="width: 55px; height: 55px; min-width: 55px;" />
                                                    <div class="min-w-0">
                                                        <h6 class="mb-0 fw-bold text-dark text-truncate" style="font-size: 0.95rem;" title='<%# Eval("Producto.Nombre") %>'>
                                                            <%# Eval("Producto.Nombre") %>
                                                        </h6>
                                                        <span class="text-muted small text-uppercase" style="font-size: 0.75rem;"><%# Eval("Producto.Marca.Nombre") %></span>
                                                    </div>
                                                </div>
                                            </td>
                                            <!-- Precio -->
                                            <td class="py-3">
                                                <span class="text-dark fw-semibold">$<%# string.Format("{0:N2}", Eval("PrecioUnitario")) %></span>
                                            </td>
                                            <!-- Cantidad -->
                                            <td class="py-3 text-center">
                                                <div class="input-group input-group-sm justify-content-center mx-auto" style="width: 100px;">
                                                    <asp:LinkButton ID="btnRestar" runat="server" CommandName="RestarCantidad" CommandArgument='<%# Eval("Producto.Id") %>' CssClass="btn btn-outline-secondary border-end-0 d-flex align-items-center px-2">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-minus-lg" viewBox="0 0 16 16">
                                                          <path fill-rule="evenodd" d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                                                        </svg>
                                                    </asp:LinkButton>
                                                    <span class="form-control text-center bg-white border-start-0 border-end-0 py-1" style="max-width: 40px; font-weight: bold; font-size: 0.85rem;">
                                                        <%# Eval("Cantidad") %>
                                                    </span>
                                                    <asp:LinkButton ID="btnSumar" runat="server" CommandName="SumarCantidad" CommandArgument='<%# Eval("Producto.Id") %>' CssClass="btn btn-outline-secondary border-start-0 d-flex align-items-center px-2">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-plus-lg" viewBox="0 0 16 16">
                                                          <path fill-rule="evenodd" d="M8 2a.5.5 0 0 1 .5.5v5h5a.5.5 0 0 1 0 1h-5v5a.5.5 0 0 1-1 0v-5h-5a.5.5 0 0 1 0-1h5v-5A.5.5 0 0 1 8 2"/>
                                                        </svg>
                                                    </asp:LinkButton>
                                                </div>
                                            </td>
                                            <!-- Subtotal -->
                                            <td class="py-3">
                                                <span class="text-primary fw-bold">$<%# string.Format("{0:N2}", Eval("Subtotal")) %></span>
                                            </td>
                                            <!-- Eliminar -->
                                            <td class="py-3 pe-4 text-end">
                                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarItem" CommandArgument='<%# Eval("Producto.Id") %>' CssClass="btn btn-sm btn-outline-danger p-1 rounded-circle" ToolTip="Eliminar producto">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
                                                      <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                                                    </svg>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- Resumen de compra -->
            <div class="col-lg-4">
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                    <h5 class="fw-bold text-dark mb-4">Resumen del Pedido</h5>
                    
                    <div class="d-flex justify-content-between mb-3">
                        <span class="text-muted">Subtotal</span>
                        <span class="text-dark fw-semibold"><asp:Label ID="lblSubtotal" runat="server" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-3">
                        <span class="text-muted">Envío</span>
                        <span class="text-success fw-semibold">Gratis</span>
                    </div>
                    
                    <hr class="my-4 border-light-subtle" />
                    
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <span class="text-dark fw-bold fs-5">Total</span>
                        <span class="text-primary fw-bold fs-4"><asp:Label ID="lblTotal" runat="server" /></span>
                    </div>
                    
                    <div class="d-grid gap-2">
                        <a href="Checkout.aspx" class="btn btn-primary py-2.5 fw-semibold">Finalizar Compra</a>
                        <a href="Productos.aspx" class="btn btn-outline-secondary py-2">Seguir Comprando</a>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    
    <style>
        .table-align-middle td, .table-align-middle th {
            vertical-align: middle;
        }
        .py-2\.5 {
            padding-top: 0.625rem;
            padding-bottom: 0.625rem;
        }
        .object-fit-cover {
            object-fit: cover;
        }
    </style>
</asp:Content>
