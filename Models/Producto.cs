namespace NegocioWeb.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string CodigoBarras { get; set; } = string.Empty;
    
    // Relaciones (Llaves foráneas)
    public int MarcaId { get; set; }
    public int CategoriaId { get; set; }
    
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public decimal PorcentajeGanancia { get; set; }
    public bool Activo { get; set; } = true;

    // Métodos de lógica de negocio
    public decimal CalcularPrecioVenta(decimal ultimoPrecioCompra)
    {
        return ultimoPrecioCompra + (ultimoPrecioCompra * PorcentajeGanancia / 100);
    }

    public void ActualizarStock(int cantidad)
    {
        StockActual += cantidad;
    }
}
