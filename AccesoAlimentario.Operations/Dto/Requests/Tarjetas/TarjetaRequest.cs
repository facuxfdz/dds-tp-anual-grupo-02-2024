using JsonSubTypes;
using Newtonsoft.Json;

namespace AccesoAlimentario.Operations.Dto.Requests.Tarjetas;

[JsonConverter(typeof(JsonSubtypes), "Tipo")]
[JsonSubtypes.KnownSubType(typeof(TarjetaConsumoRequest), "Consumo")]
[JsonSubtypes.KnownSubType(typeof(TarjetaColaboracionRequest), "Colaboracion")]
public abstract class TarjetaRequest : IDtoRequest
{
    public string Codigo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    
    public abstract bool Validar();
}

public class TarjetaConsumoRequest : TarjetaRequest
{
    public int ResponsableId { get; set; } = 0;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Codigo) && ResponsableId > 0;
    }
}

public class TarjetaColaboracionRequest : TarjetaRequest
{
    public int ResponsableId { get; set; } = 0;
    
    public override bool Validar()
    {
        return !string.IsNullOrEmpty(Codigo) && ResponsableId > 0;
    }
}