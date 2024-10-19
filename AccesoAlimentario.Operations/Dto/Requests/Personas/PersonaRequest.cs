using AccesoAlimentario.Core.Entities.Personas;
using JsonSubTypes;
using Newtonsoft.Json;

namespace AccesoAlimentario.Operations.Dto.Requests.Personas;

[JsonConverter(typeof(JsonSubtypes), "Tipo")]
[JsonSubtypes.KnownSubType(typeof(PersonaHumanaRequest), "Humana")]
[JsonSubtypes.KnownSubType(typeof(PersonaJuridicaRequest), "Humana")]
public abstract class PersonaRequest : IDtoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo = string.Empty;

    public abstract bool Validar();
}

public class PersonaHumanaRequest : PersonaRequest
{
    public string Apellido = string.Empty;
    public SexoDocumento Sexo = SexoDocumento.Otro;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Apellido);
    }
}

public class PersonaJuridicaRequest : PersonaRequest
{
    public TipoJuridico TipoJuridico = TipoJuridico.Gubernamental;
    public string Rubro = string.Empty;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Rubro);
    }
}