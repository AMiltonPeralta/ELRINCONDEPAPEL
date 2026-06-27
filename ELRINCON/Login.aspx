<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ELRINCON.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="text-center mb-5">
            <h2 class="text-dark fw-bold">Iniciar Sesión</h2>
            <p class="text-muted fs-5">Selecciona el tipo de cuenta con el que deseas ingresar a El Rincón del Papel</p>
        </div>

        <div class="row g-4 justify-content-center mx-auto" style="max-width: 800px;">
            <!-- Opción 1: Cliente / Usuario -->
            <div class="col-md-6">
                <div class="card h-100 shadow-sm border border-light-subtle rounded-3 hover-card transition-all text-center p-4 bg-white">
                    <div class="mb-3 d-inline-flex align-items-center justify-content-center bg-primary bg-opacity-10 text-primary rounded-circle" style="width: 70px; height: 70px;">
                        <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16">
                          <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6"/>
                        </svg>
                    </div>
                    <h4 class="text-dark fw-bold mb-3">Ingresar como Usuario</h4>
                    <p class="text-muted small mb-4">Accede como cliente para navegar por el catálogo completo de productos, agregarlos al carrito y finalizar compras en la tienda.</p>
                    <div class="mt-auto d-grid">
                        <asp:Button ID="btnIngresarUsuario" runat="server" Text="Entrar como Cliente" CssClass="btn btn-primary fw-semibold py-2.5" OnClick="btnIngresarUsuario_Click" />
                    </div>
                </div>
            </div>

            <!-- Opción 2: Administrador -->
            <div class="col-md-6">
                <div class="card h-100 shadow-sm border border-light-subtle rounded-3 hover-card transition-all text-center p-4 bg-white">
                    <div class="mb-3 d-inline-flex align-items-center justify-content-center bg-danger bg-opacity-10 text-danger rounded-circle" style="width: 70px; height: 70px;">
                        <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" fill="currentColor" class="bi bi-shield-lock-fill" viewBox="0 0 16 16">
                          <path fill-rule="evenodd" d="M8 0c-.69 0-1.843.265-2.928.56-1.11.3-2.229.655-2.887.87a1.54 1.54 0 0 0-1.044 1.262c-.596 4.477.787 7.795 2.465 9.99a11.8 11.8 0 0 0 2.517 2.453c.386.273.744.482 1.048.625.28.132.581.24.829.24s.548-.108.829-.24a7 7 0 0 0 1.048-.625 11.8 11.8 0 0 0 2.517-2.453c1.678-2.195 3.061-5.513 2.465-9.99a1.54 1.54 0 0 0-1.044-1.263 62 62 0 0 0-2.887-.87C9.843.266 8.69 0 8 0m0 5a1.5 1.5 0 0 1 .5 2.915V9a1.5 1.5 0 0 1-3 0V7.915A1.5 1.5 0 0 1 8 5"/>
                        </svg>
                    </div>
                    <h4 class="text-dark fw-bold mb-3">Ingresar como Administrador</h4>
                    <p class="text-muted small mb-4">Accede con permisos de administración para gestionar el stock, agregar nuevos productos al catálogo y modificar la información existente.</p>
                    <div class="mt-auto d-grid">
                        <asp:Button ID="btnIngresarAdmin" runat="server" Text="Entrar como Admin" CssClass="btn btn-danger fw-semibold py-2.5" OnClick="btnIngresarAdmin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .hover-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
        }
        .transition-all {
            transition: all 0.25s ease-in-out;
        }
        .py-2\.5 {
            padding-top: 0.625rem;
            padding-bottom: 0.625rem;
        }
    </style>
</asp:Content>
