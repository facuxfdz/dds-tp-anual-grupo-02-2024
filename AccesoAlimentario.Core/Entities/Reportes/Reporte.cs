namespace AccesoAlimentario.Core.Entities.Reportes;

public class Reporte
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public TipoReporte Tipo { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaExpiracion { get; set; } = DateTime.UtcNow.AddDays(7);
    public string Cuerpo { get; set; } = string.Empty;
}