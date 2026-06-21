using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; } = "";
        public Usuario Cliente { get; set; } = new Usuario();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public MetodoPago Pago { get; set; } = new MetodoPago();
        public Envio DatosEnvio { get; set; } = new Envio();
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
