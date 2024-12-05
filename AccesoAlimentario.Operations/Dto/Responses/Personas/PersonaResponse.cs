using System.Text.Json.Serialization;
using AccesoAlimentario.Operations.Dto.Responses.Direcciones;
using AccesoAlimentario.Operations.Dto.Responses.DocumentosIdentidad;
using AccesoAlimentario.Operations.Dto.Responses.MediosContacto;

namespace AccesoAlimentario.Operations.Dto.Responses.Personas;

[JsonDerivedType(typeof(PersonaHumanaResponse))]
[JsonDerivedType(typeof(PersonaJuridicaResponse))]
public abstract class PersonaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Nombre { get; set; } = string.Empty;
    public DireccionResponse? Direccion { get; set; } = null!;
    public DocumentoIdentidadResponse? DocumentoIdentidad { get; set; } = null!;
    public List<MedioContactoResponse> MediosDeContacto { get; set; } = [];
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
    public string TipoPersona { get; set; } = string.Empty;
}