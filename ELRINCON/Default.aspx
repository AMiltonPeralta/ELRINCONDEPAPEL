<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ELRINCON._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Panel de Bienvenida -->
    <div class="card-custom text-center py-5 mb-5 shadow-sm border border-light-subtle rounded-3 bg-white">
        <h1 class="display-4 mb-3 fw-bold text-dark text-uppercase tracking-wide">El Rinc&oacute;n del Papel</h1>
        <p class="lead text-muted mb-0">
            Bienvenido a El Rinc&oacute;n del Papel, donde puede encontrar los mejores productos al mejor precio.
        </p>
    </div>

    <!-- Sección de Productos Destacados -->
    <div class="container mb-5">
        <div class="border-bottom pb-2 mb-4">
            <h3 class="text-dark fw-bold mb-1">Productos Destacados</h3>
            <p class="text-muted small mb-0">Nuestros artículos más recomendados en librería, computación, gaming y juegos de mesa.</p>
        </div>

        <!-- Grilla de Tarjetas (Fila de 4 columnas) -->
        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4 mb-5">
            <asp:Repeater ID="repProductos" runat="server" OnItemCommand="repProductos_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 border rounded shadow-sm hover-card transition-all" style="background: #ffffff;">
                            <!-- Contenedor de la Imagen del Producto -->
                            <div class="position-relative overflow-hidden bg-light border-bottom rounded-top" style="height: 180px;">
                                <img src='<%# string.IsNullOrEmpty(Eval("ImagenUrl") as string) ? "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80" : Eval("ImagenUrl") %>' 
                                     alt='<%# Eval("Nombre") %>' 
                                     class="w-100 h-100 object-fit-cover transition-transform hover-zoom" />
                                <span class="position-absolute top-2 end-2 badge bg-dark opacity-75">
                                    Stock: <%# Eval("StockActual") %>
                                </span>
                            </div>
                            
                            <!-- Detalles del Producto -->
                            <div class="card-body d-flex flex-column p-3">
                                <span class="text-uppercase text-muted fw-bold small mb-1 d-block"><%# ((Dominio.Producto)Container.DataItem).Marca.Nombre %></span>
                                <h5 class="card-title text-dark fw-bold mb-2 text-truncate" title='<%# Eval("Nombre") %>'><%# Eval("Nombre") %></h5>
                                
                                <div class="mt-auto pt-3 border-top">
                                    <div class="d-flex justify-content-between align-items-center mb-2">
                                        <span class="badge bg-secondary opacity-75"><%# ((Dominio.Producto)Container.DataItem).Categoria.Nombre %></span>
                                        <asp:HyperLink ID="lnkEditar" runat="server" NavigateUrl='<%# "FormularioProducto.aspx?id=" + Eval("Id") + "&cat=" + Eval("Categoria.Id") %>' CssClass="btn btn-sm btn-outline-warning fw-bold px-2 py-0" style="font-size: 0.75rem;" Visible='<%# Negocio.Seguridad.esAdmin(Session["usuario"]) %>'>Editar</asp:HyperLink>
                                    </div>
                                    <asp:LinkButton ID="btnAgregarCarrito" runat="server" CssClass="btn btn-sm btn-primary w-100 fw-bold d-flex align-items-center justify-content-center gap-1" CommandName="AgregarCarrito" CommandArgument='<%# Eval("Id") %>'>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" fill="currentColor" class="bi bi-cart-plus" viewBox="0 0 16 16">
                                          <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9z"/>
                                          <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
                                        </svg>
                                        Agregar al Carrito
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Botón Ver Más (Azul) -->
        <div class="text-center mt-5 mb-4">
            <a href="Productos.aspx" class="btn btn-primary btn-lg px-5 py-3 fw-bold shadow-sm" style="font-size: 1.1rem; border-radius: 8px;">Ver m&aacute;s</a>
        </div>
    </div>

    <!-- Estilos de Tarjetas -->
    <style>
        .hover-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
        }
        .transition-all {
            transition: all 0.25s ease-in-out;
        }
        .object-fit-cover {
            object-fit: cover;
        }
        .hover-zoom {
            transition: transform 0.3s ease-in-out;
        }
        .hover-card:hover .hover-zoom {
            transform: scale(1.05);
        }
        .top-2 { top: 0.5rem; }
        .end-2 { right: 0.5rem; }
    </style>
</asp:Content>
