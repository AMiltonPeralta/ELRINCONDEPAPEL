namespace NegocioWeb.Models;

public class Compra
{
    public int Id { get; set; }
    public int ProveedorId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public int UsuarioId { get; set; }

    // Renglones de la compra
    public List<DetalleCompra> Detalles { get; set; } = new();

    public void CalcularTotal()
    {
        Total = 0;
        foreach (var det in Detalles)
        {
            Total += det.Cantidad * det.PrecioUnitario;
        }
    }
}
