using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Operations.Dto.Responses.Personas;

public class PersonaJuridicaResponse : PersonaResponse
{
    public string RazonSocial { get; set; } = string.Empty;
    public TipoJuridico Tipo { get; set; } = TipoJuridico.Gubernamental;
    public string Rubro { get; set; } = string.Empty;
}