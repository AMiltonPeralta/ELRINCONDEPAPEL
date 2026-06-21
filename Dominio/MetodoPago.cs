using System;

namespace Dominio
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public bool Activo { get; set; } = true;
    }
}
