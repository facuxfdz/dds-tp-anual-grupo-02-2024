using System.Text.Json.Serialization;
using AccesoAlimentario.Operations.Dto.Responses.Personas;

namespace AccesoAlimentario.Operations.Dto.Responses.Roles;

[JsonDerivedType(typeof(ColaboradorResponse))]
[JsonDerivedType(typeof(PersonaVulnerableResponse))]
[JsonDerivedType(typeof(TecnicoResponse))]
public abstract class RolResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public PersonaResponse Persona { get; set; } = null!;
    public string Tipo { get; set; } = string.Empty;
}