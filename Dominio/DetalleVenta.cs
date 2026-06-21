using System;

namespace Dominio
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public Producto Articulo { get; set; } = new Producto();
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal 
        { 
            get { return Cantidad * PrecioUnitario; } 
        }
    }
}
