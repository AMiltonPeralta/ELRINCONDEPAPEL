<%@ Page Title="Compra Confirmada" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="ELRINCON.Confirmacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="card shadow-sm border border-light-subtle rounded-3 p-5 text-center mx-auto" style="max-width: 600px; background-color: #ffffff;">
            <!-- Icono de éxito animado o estilizado -->
            <div class="mb-4 d-inline-flex align-items-center justify-content-center bg-success bg-opacity-10 text-success rounded-circle" style="width: 80px; height: 80px;">
                <svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" fill="currentColor" class="bi bi-check-circle-fill" viewBox="0 0 16 16">
                  <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
                </svg>
            </div>
            
            <h2 class="text-dark fw-bold mb-3">¡Compra Realizada con Éxito!</h2>
            <p class="text-muted fs-5 mb-4">Hemos recibido tu pedido correctamente. Se ha enviado un correo de confirmación con los detalles de tu compra.</p>
            
            <!-- Detalles de seguimiento -->
            <div class="bg-light rounded-3 p-4 mb-4 border border-light-subtle text-start">
                <h6 class="fw-bold text-dark mb-3 text-uppercase small tracking-wider">Detalles de Entrega</h6>
                
                <div class="d-flex justify-content-between align-items-center py-2 border-bottom border-light-subtle">
                    <span class="text-muted">Número de Seguimiento:</span>
                    <span class="text-primary fw-bold fs-5"><asp:Label ID="lblSeguimiento" runat="server" /></span>
                </div>
                
                <div class="d-flex justify-content-between align-items-center py-2 border-bottom border-light-subtle">
                    <span class="text-muted">Estado del Envío:</span>
                    <span class="badge bg-warning-subtle text-warning-emphasis border border-warning-subtle fw-semibold">Pendiente de Despacho</span>
                </div>
                
                <div class="d-flex justify-content-between align-items-center py-2">
                    <span class="text-muted">Fecha estimada de entrega:</span>
                    <span class="text-dark fw-semibold">2 a 5 días hábiles</span>
                </div>
            </div>
            
            <!-- Mensaje Pago Fácil -->
            <asp:Panel ID="pnlConfirmacionPagoFacil" runat="server" Visible="false" CssClass="alert alert-info border-info-subtle mb-4 text-start shadow-sm">
                <div class="d-flex gap-2 align-items-center">
                    <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" fill="currentColor" class="bi bi-wallet2 text-info flex-shrink-0" viewBox="0 0 16 16">
                        <path d="M12.136.326A1.5 1.5 0 0 1 14 1.78V3h.5A1.5 1.5 0 0 1 16 4.5v9a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 13.5v-9A1.5 1.5 0 0 1 1.5 3H2V1.78a1.5 1.5 0 0 1 1.864-1.454zM3 3h9v-.22a.5.5 0 0 0-.118-.323L11.5 2H4.5L4.118 2.457A.5.5 0 0 0 4 2.78zM1.5 4A.5.5 0 0 0 1 4.5v9a.5.5 0 0 0 .5.5h13a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5z"/>
                    </svg>
                    <div>
                        <strong>Instrucciones de Pago (Pago Fácil):</strong> Dirígete a cualquier sucursal física de Pago Fácil con tu número de factura <strong><asp:Label ID="lblFactura" runat="server" /></strong> para abonar el total correspondiente.
                    </div>
                </div>
            </asp:Panel>
            
            <div class="d-flex justify-content-center gap-3">
                <a href="Productos.aspx" class="btn btn-primary px-4 py-2 fw-semibold">Seguir Comprando</a>
            </div>
        </div>
    </div>
</asp:Content>
