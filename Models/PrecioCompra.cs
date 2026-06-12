namespace NegocioWeb.Models;

public class PrecioCompra
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
