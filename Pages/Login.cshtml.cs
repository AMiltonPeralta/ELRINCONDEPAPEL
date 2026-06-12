using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NegocioWeb.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Usuario { get; set; } = string.Empty;

    [BindProperty]
    public string Contrasena { get; set; } = string.Empty;

    public string MensajeError { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Contrasena))
        {
            MensajeError = "Por favor, completa todos los campos.";
            return Page();
        }

        // Validación simple simulada
        if ((Usuario == "admin" && Contrasena == "admin123") || 
            (Usuario == "vendedor" && Contrasena == "vendedor123"))
        {
            // Redirigir a la página de inicio
            return RedirectToPage("/Index");
        }

        MensajeError = "Usuario o contraseña incorrectos.";
        return Page();
    }
}
