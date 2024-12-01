using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Operations.Dto.Responses.Personas;

public class PersonaHumanaResponse : PersonaResponse
{
    public string Apellido { get; set; } = string.Empty;
    public SexoDocumento Sexo { get; set; } = SexoDocumento.Otro;
}