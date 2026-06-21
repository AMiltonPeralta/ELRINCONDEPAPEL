using System;

namespace Dominio
{
    public class Envio
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public string Direccion { get; set; } = "";
        public string Localidad { get; set; } = "";
        public string CodigoPostal { get; set; } = "";
        public string NumeroSeguimiento { get; set; } = "";
        public string Estado { get; set; } = "Pendiente"; // "Pendiente", "Despachado", "Entregado"
    }
}
