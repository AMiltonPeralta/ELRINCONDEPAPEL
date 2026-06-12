namespace NegocioWeb.Models;

public class Venta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;
    public int UsuarioId { get; set; }

    // Renglones de la venta
    public List<DetalleVenta> Detalles { get; set; } = new();

    public void CalcularTotal()
    {
        Total = 0;
        foreach (var det in Detalles)
        {
            Total += det.Cantidad * det.PrecioUnitario;
        }
    }

    public string GenerarNumeroFactura()
    {
        // Genera el número con formato 0001-0000000X usando el ID
        NumeroFactura = $"0001-{Id.ToString().PadLeft(8, '0')}";
        return NumeroFactura;
    }
}
