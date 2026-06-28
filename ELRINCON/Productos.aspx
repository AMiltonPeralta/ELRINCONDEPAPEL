<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="ELRINCON.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
        <ContentTemplate>
            <!-- Panel de Selección de Categorías -->
            <asp:Panel ID="pnlSeleccion" runat="server" CssClass="card-custom text-center py-5">
                <h2 class="text-dark mb-4 fw-bold">Productos por Categoría</h2>
                
                <div class="mb-4 text-start mx-auto" style="max-width: 360px;">
                    <label for="ddlCategorias" class="form-label fw-bold">Seleccionar Categoría</label>
                    <div class="d-flex gap-2 mb-3">
                        <asp:DropDownList ID="ddlCategorias" CssClass="form-select" runat="server" />
                        <asp:Button ID="btnAceptar" runat="server" Text="Ir" CssClass="btn btn-dark" OnClick="btnAceptar_Click" />
                    </div>
                    
                    <!-- Botones de administración de categorías -->
                    <div class="d-flex gap-2 mb-2">
                        <asp:Button ID="btnMostrarCrearCat" runat="server" Text="Nueva Categoría" CssClass="btn btn-outline-primary btn-sm flex-fill" OnClick="btnMostrarCrearCat_Click" />
                        <asp:Button ID="btnEliminarCat" runat="server" Text="Eliminar Categoría" CssClass="btn btn-outline-danger btn-sm flex-fill" OnClick="btnEliminarCat_Click" />
                    </div>
                    
                    <!-- Formulario Inline para Nueva Categoría -->
                    <asp:Panel ID="pnlCrearCategoria" runat="server" Visible="false" CssClass="mt-3 p-3 border rounded bg-light">
                        <label class="form-label fw-bold text-muted small">Nombre de la nueva categoría</label>
                        <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control form-control-sm mb-2" placeholder="Ej: Deportes, Accesorios" />
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-success btn-sm flex-fill" OnClick="btnGuardarCategoria_Click" />
                            <asp:Button ID="btnCancelarCategoria" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-sm" OnClick="btnCancelarCategoria_Click" />
                        </div>
                    </asp:Panel>

                    <!-- Formulario Inline para Eliminar Categoría -->
                    <asp:Panel ID="pnlConfirmarEliminarCat" runat="server" Visible="false" CssClass="mt-3 p-3 border border-danger-subtle rounded bg-danger-subtle text-danger-emphasis">
                        <p class="small fw-semibold mb-2 text-center">¿Seguro que deseas eliminar la categoría "<asp:Label ID="lblCatAEliminar" runat="server" />"?</p>
                        <div class="form-check mb-2 d-flex align-items-center justify-content-center">
                            <input type="checkbox" id="chkConfirmaEliminarCat" runat="server" class="form-check-input flex-shrink-0" style="width: 1.15rem; height: 1.15rem; cursor: pointer;" />
                            <label class="form-check-label fw-semibold small ms-2 lh-1" for="MainContent_chkConfirmaEliminarCat" style="cursor: pointer; user-select: none;">
                                Confirmar eliminación
                            </label>
                        </div>
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnConfirmaEliminarCat" runat="server" Text="Eliminar de verdad" CssClass="btn btn-danger btn-sm flex-fill" OnClick="btnConfirmaEliminarCat_Click" />
                            <asp:Button ID="btnCancelarEliminarCat" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-sm" OnClick="btnCancelarEliminarCat_Click" />
                        </div>
                    </asp:Panel>

                    <!-- Mensaje de Advertencia/Error para Categorías -->
                    <asp:Label ID="lblMensajeCat" runat="server" CssClass="d-block small text-danger fw-semibold mt-2 text-center" />
                </div>

                <a href="Default.aspx" class="btn btn-secondary mt-3">Volver al Inicio</a>
            </asp:Panel>

            <!-- Panel de Listado de Productos (Estilo Pokedex) -->
            <asp:Panel ID="pnlListado" runat="server" Visible="false">
                <div class="card-custom py-4 mb-4 mt-4">
                    <div class="d-flex justify-content-between align-items-center flex-wrap gap-3">
                        <div>
                            <span class="text-uppercase text-muted fw-bold small">Sección de Productos</span>
                            <h2 class="text-dark fw-bold mb-0">Categoría: <asp:Label ID="lblNombreCategoria" runat="server" /></h2>
                        </div>
                        <div class="d-flex gap-2">
                            <asp:HyperLink ID="lnkAgregarProducto" runat="server" CssClass="btn btn-primary">Agregar Producto</asp:HyperLink>
                            <a href="Productos.aspx" id="btnVolverSeleccion" runat="server" class="btn btn-outline-secondary">Volver a Selección</a>
                        </div>
                    </div>
                </div>

                <!-- Grilla de Tarjetas Tipo Pokedex -->
                <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4 mb-4">
                    <asp:Repeater ID="repProductos" runat="server" OnItemCommand="repProductos_ItemCommand">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card h-100 border rounded shadow-sm hover-card transition-all" style="background: #ffffff;">
                                    <!-- Contenedor de la Imagen del Producto -->
                                    <div class="position-relative overflow-hidden bg-light border-bottom rounded-top" style="height: 180px;">
                                        <img src='<%# string.IsNullOrEmpty(Eval("ImagenUrl") as string) ? "https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80" : Eval("ImagenUrl") %>' 
                                             alt='<%# Eval("Nombre") %>' 
                                             class="w-100 h-100 object-fit-cover transition-transform hover-zoom"
                                             onerror="this.src='https://images.unsplash.com/photo-1595231712426-63d27862de67?w=300&q=80';" />
                                        <span class="position-absolute top-2 end-2 badge bg-dark opacity-75">
                                            Stock: <%# Eval("StockActual") %>
                                        </span>
                                    </div>
                                    
                                    <!-- Detalles del Producto -->
                                    <div class="card-body d-flex flex-column p-3">
                                        <span class="text-uppercase text-muted fw-bold small mb-1 d-block"><%# ((Dominio.Producto)Container.DataItem).Marca.Nombre %></span>
                                        <h5 class="card-title text-dark fw-bold mb-1 text-truncate" title='<%# Eval("Nombre") %>'><%# Eval("Nombre") %></h5>
                                        <div class="mb-2">
                                            <span class="fs-5 fw-bold text-success">$<%# string.Format("{0:N2}", Eval("Precio")) %></span>
                                        </div>
                                        
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
                
                <!-- Paginación -->
                <asp:Panel ID="pnlPaginacion" runat="server" CssClass="d-flex justify-content-center mt-4 mb-5">
                    <nav aria-label="Navegacion de paginas">
                        <ul class="pagination">
                            <li class="page-item">
                                <asp:LinkButton ID="btnAnterior" runat="server" CssClass="page-link" OnClick="btnAnterior_Click">Anterior</asp:LinkButton>
                            </li>
                            
                            <asp:Repeater ID="repPaginas" runat="server" OnItemCommand="repPaginas_ItemCommand">
                                <ItemTemplate>
                                    <li class='<%# "page-item " + ((int)Container.DataItem == CurrentPage ? "active" : "") %>'>
                                        <asp:LinkButton ID="btnPagina" runat="server" CssClass="page-link" CommandName="CambiarPagina" CommandArgument='<%# Container.DataItem %>'>
                                            <%# (int)Container.DataItem + 1 %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                            <li class="page-item">
                                <asp:LinkButton ID="btnSiguiente" runat="server" CssClass="page-link" OnClick="btnSiguiente_Click">Siguiente</asp:LinkButton>
                            </li>
                        </ul>
                    </nav>
                </asp:Panel>

                <!-- Mensaje si no hay productos -->
                <asp:Panel ID="pnlSinProductos" runat="server" Visible="false" CssClass="text-center py-5 border rounded bg-light">
                    <p class="text-muted fs-4">No hay productos cargados en esta categoría.</p>
                    <asp:HyperLink ID="lnkAgregarProductoVacio" runat="server" CssClass="btn btn-primary" Text="Cargar el primer producto" />
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Estilos adicionales integrados en el head o master, los ponemos acá para facilidad -->
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
