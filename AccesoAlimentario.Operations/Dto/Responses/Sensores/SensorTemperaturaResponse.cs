namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class SensorTemperaturaResponse : SensorResponse
{
    public List<RegistroTemperaturaResponse> RegistrosTemperatura { get; set; } = [];
}