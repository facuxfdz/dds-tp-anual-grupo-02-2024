using System.Text.Json.Serialization;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.SuscripcionesColaboradores;

[JsonDerivedType(typeof(SuscripcionExcedenteViandasResponse))]
[JsonDerivedType(typeof(SuscripcionFaltanteViandasResponse))]
[JsonDerivedType(typeof(SuscripcionIncidenteHeladeraResponse))]
public abstract class SuscripcionResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public HeladeraResponse Heladera { get; set; } = null!;
    public string Tipo { get; set; } = string.Empty;
}