using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Clave { get; set; } = ""; // Contraseña
        public string Rol { get; set; } = "Vendedor"; // "Administrador" o "Vendedor"
        public bool Activo { get; set; } = true;
    }
}

