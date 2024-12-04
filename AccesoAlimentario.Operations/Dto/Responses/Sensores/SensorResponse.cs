using System.Text.Json.Serialization;

namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

[JsonDerivedType(typeof(SensorMovimientoResponse))]
[JsonDerivedType(typeof(SensorTemperaturaResponse))]
public abstract class SensorResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Tipo { get; set; } = string.Empty;
}