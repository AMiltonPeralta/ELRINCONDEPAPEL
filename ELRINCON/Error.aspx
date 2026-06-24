<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="ELRINCON.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="card shadow-sm border border-light-subtle rounded-3 p-5 text-center mx-auto" style="max-width: 500px; background-color: #ffffff;">
            <svg xmlns="http://www.w3.org/2000/svg" width="64" height="64" fill="currentColor" class="bi bi-exclamation-triangle-fill text-danger mb-4" viewBox="0 0 16 16">
              <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
            </svg>
            <h3 class="text-dark fw-bold mb-3">Ha ocurrido un problema</h3>
            <p class="text-muted mb-4">
                <asp:Label ID="lblError" runat="server" CssClass="fw-semibold text-danger fs-5" />
            </p>
            <a href="javascript:history.back()" class="btn btn-primary px-4">Volver Atrás</a>
        </div>
    </div>
</asp:Content>
