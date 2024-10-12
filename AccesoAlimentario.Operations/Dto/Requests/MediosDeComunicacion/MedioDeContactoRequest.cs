using JsonSubTypes;
using Newtonsoft.Json;

namespace AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;

[JsonConverter(typeof(JsonSubtypes), "Tipo")]
[JsonSubtypes.KnownSubType(typeof(TelefonoRequest), "Telefono")]
[JsonSubtypes.KnownSubType(typeof(EmailRequest), "Email")]
[JsonSubtypes.KnownSubType(typeof(WhatsappRequest), "Whatsapp")]
public abstract class MedioDeContactoRequest : IDtoRequest
{
    public bool Preferida { get; set; } = false;
    public string Tipo { get; set; } = string.Empty;
    
    public abstract bool Validar();
}

public class TelefonoRequest : MedioDeContactoRequest
{
    public string Numero { get; set; } = string.Empty;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Numero);
    }
}

public class EmailRequest : MedioDeContactoRequest
{
    public string Direccion { get; set; } = string.Empty;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Direccion);
    }
}

public class WhatsappRequest : MedioDeContactoRequest
{
    public string Numero { get; set; } = string.Empty;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Numero);
    }
}