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
}

public class TarjetaConsumoRequest : TarjetaRequest
{
    public Guid ResponsableId { get; set; } = Guid.Empty;
}

public class TarjetaColaboracionRequest : TarjetaRequest
{
    public Guid PropietarioId { get; set; } = Guid.Empty;
}