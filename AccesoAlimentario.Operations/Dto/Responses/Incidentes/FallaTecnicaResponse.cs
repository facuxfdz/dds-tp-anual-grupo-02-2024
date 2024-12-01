namespace AccesoAlimentario.Operations.Dto.Responses.Incidentes;

public class FallaTecnicaResponse : IncidenteResponse
{
    public string? Descripcion { get; set; } = null;
    public string? Foto { get; set; } = null;
}