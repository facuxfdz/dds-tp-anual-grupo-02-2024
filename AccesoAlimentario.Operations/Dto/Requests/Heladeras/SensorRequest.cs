using JsonSubTypes;
using Newtonsoft.Json;

namespace AccesoAlimentario.Operations.Dto.Requests.Heladeras;

[JsonConverter(typeof(JsonSubtypes), "Tipo")]
[JsonSubtypes.KnownSubType(typeof(SensorTemperaturaRequest), "Temperatura")]
[JsonSubtypes.KnownSubType(typeof(SensorMovimientoRequest), "Movimiento")]
public abstract class SensorRequest
{
    public string Tipo = string.Empty;
}

public class SensorTemperaturaRequest : SensorRequest
{
    
}

public class SensorMovimientoRequest : SensorRequest
{
    
}