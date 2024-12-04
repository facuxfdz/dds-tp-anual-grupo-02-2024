using System.Text.Json.Serialization;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

[JsonDerivedType(typeof(AdministracionHeladeraResponse))]
[JsonDerivedType(typeof(DistribucionViandasResponse))]
[JsonDerivedType(typeof(DonacionMonetariaResponse))]
[JsonDerivedType(typeof(DonacionViandaResponse))]
[JsonDerivedType(typeof(OfertaPremioResponse))]
[JsonDerivedType(typeof(RegistroPersonaVulnerableResponse))]
public abstract class FormaContribucionResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
    public string Tipo { get; set; } = string.Empty;
}