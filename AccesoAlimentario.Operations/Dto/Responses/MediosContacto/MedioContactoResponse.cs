using System.Text.Json.Serialization;

namespace AccesoAlimentario.Operations.Dto.Responses.MediosContacto;

[JsonDerivedType(typeof(EmailResponse))]
public abstract class MedioContactoResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public bool Preferida { get; set; } = false;
    public string Tipo { get; set; } = string.Empty;
}