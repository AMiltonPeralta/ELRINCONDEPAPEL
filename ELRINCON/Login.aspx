<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ELRINCON.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        
        <!-- Alertas de Error Generales (Centrado) -->
        <asp:Panel ID="pnlLoginError" runat="server" CssClass="alert alert-danger shadow-sm d-flex align-items-center gap-2 border border-danger-subtle rounded-3 mb-4 mx-auto" style="max-width: 500px;" Visible="false">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-exclamation-triangle-fill text-danger flex-shrink-0" viewBox="0 0 16 16">
              <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
            </svg>
            <div>
                <asp:Label ID="lblLoginErrorMessage" runat="server" />
            </div>
        </asp:Panel>

        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">

                <!-- PANEL 1: INGRESO TRADICIONAL -->
                <asp:Panel ID="pnlLogin" runat="server">
                    <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                        <h4 class="text-dark fw-bold mb-3 border-bottom pb-2">Ingresar a tu Cuenta</h4>
                        <p class="text-muted small mb-4">Si ya tienes una cuenta registrada, escribe tu usuario (email) y contraseña.</p>

                        <div class="mb-3">
                            <label for="txtEmail" class="form-label fw-semibold">Correo Electrónico</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@correo.com" TextMode="Email" MaxLength="100" />
                        </div>

                        <div class="mb-4">
                            <label for="txtClave" class="form-label fw-semibold">Contraseña</label>
                            <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" placeholder="******" MaxLength="50" />
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnLoginTradicional" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary fw-semibold py-2" OnClick="btnLoginTradicional_Click" />
                        </div>
                    </div>

                    <div class="text-center mt-3">
                        <asp:LinkButton ID="btnMostrarRegistro" runat="server" CssClass="btn btn-link text-decoration-none fw-semibold" OnClick="btnMostrarRegistro_Click" UseSubmitBehavior="false">
                            ¿No tienes cuenta? Crear Cuenta Nueva
                        </asp:LinkButton>
                    </div>
                </asp:Panel>

                <!-- PANEL 2: REGISTRO DE CUENTA -->
                <asp:Panel ID="pnlRegistro" runat="server" Visible="false">
                    <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-white">
                        <h4 class="text-dark fw-bold mb-3 border-bottom pb-2">Crear una Cuenta</h4>
                        <p class="text-muted small mb-4">Completa el formulario para registrarte como cliente y finalizar tu compra.</p>

                        <div class="mb-3">
                            <label for="txtRegNombre" class="form-label fw-semibold">Nombre Completo</label>
                            <asp:TextBox ID="txtRegNombre" runat="server" CssClass="form-control" placeholder="Ej: Milton Peralta" MaxLength="50" />
                        </div>

                        <div class="mb-3">
                            <label for="txtRegEmail" class="form-label fw-semibold">Correo Electrónico</label>
                            <asp:TextBox ID="txtRegEmail" runat="server" CssClass="form-control" placeholder="ejemplo@correo.com" TextMode="Email" MaxLength="100" />
                        </div>

                        <div class="mb-3">
                            <label for="txtRegClave" class="form-label fw-semibold">Contraseña</label>
                            <asp:TextBox ID="txtRegClave" runat="server" CssClass="form-control" TextMode="Password" placeholder="Crea tu contraseña" MaxLength="50" />
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Crear Cuenta" CssClass="btn btn-success fw-semibold py-2" OnClick="btnRegistrar_Click" />
                        </div>
                    </div>

                    <div class="text-center mt-3">
                        <asp:LinkButton ID="btnMostrarLogin" runat="server" CssClass="btn btn-link text-decoration-none fw-semibold" OnClick="btnMostrarLogin_Click" UseSubmitBehavior="false">
                            ¿Ya tienes cuenta? Volver al Inicio de Sesión
                        </asp:LinkButton>
                    </div>
                </asp:Panel>

            </div>
        </div>

        <!-- Accesos Rápidos de Desarrollo (Centrado) -->
        <div class="row justify-content-center mt-5">
            <div class="col-md-6 col-lg-5">
                <div class="card shadow-sm border border-light-subtle rounded-3 p-4 bg-light text-center">
                    <h6 class="fw-bold text-secondary text-uppercase small mb-3">Accesos rápidos para pruebas (Desarrollo)</h6>
                    <div class="d-flex justify-content-center gap-3 flex-wrap">
                        <asp:Button ID="btnIngresarUsuario" runat="server" Text="Entrar como Milton Cliente" CssClass="btn btn-outline-primary btn-sm fw-semibold" OnClick="btnIngresarUsuario_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="btnIngresarAdmin" runat="server" Text="Entrar como Administrador" CssClass="btn btn-outline-danger btn-sm fw-semibold" OnClick="btnIngresarAdmin_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
