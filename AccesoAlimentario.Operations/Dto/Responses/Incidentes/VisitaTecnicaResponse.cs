using AccesoAlimentario.Operations.Dto.Responses.Roles;

namespace AccesoAlimentario.Operations.Dto.Responses.Incidentes;

public class VisitaTecnicaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public TecnicoResponse Tecnico { get; set; } = null!;
    public string Foto { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Comentario { get; set; } = string.Empty;
}