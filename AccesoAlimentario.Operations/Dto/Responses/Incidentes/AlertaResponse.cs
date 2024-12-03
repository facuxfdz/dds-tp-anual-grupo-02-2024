using AccesoAlimentario.Core.Entities.Incidentes;

namespace AccesoAlimentario.Operations.Dto.Responses.Incidentes;

public class AlertaResponse : IncidenteResponse
{
    public TipoAlerta Tipo { get; set; } = TipoAlerta.Conexion;
}