using Microsoft.AspNetCore.Mvc.RazorPages;
using NegocioWeb.Data;
using NegocioWeb.Models;

namespace NegocioWeb.Pages;

public class ClientesModel : PageModel
{
    private readonly ClienteRepository _clienteRepository;

    public ClientesModel(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public List<Cliente> Clientes { get; set; } = new();
    public string MensajeError { get; set; } = string.Empty;

    public void OnGet()
    {
        try
        {
            Clientes = _clienteRepository.Listar();
        }
        catch (Exception ex)
        {
            MensajeError = $"Error al conectar a SQL Server: {ex.Message}";
            Clientes = new List<Cliente>();
        }
    }
}
