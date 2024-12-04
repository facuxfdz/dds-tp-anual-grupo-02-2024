namespace AccesoAlimentario.Operations.Dto.Responses.Incidentes;

public abstract class IncidenteResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public List<VisitaTecnicaResponse> VisitasTecnicas { get; set; } = [];
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public bool Resuelto { get; set; } = false;
    public string TipoIncidente { get; set; } = null!;
}