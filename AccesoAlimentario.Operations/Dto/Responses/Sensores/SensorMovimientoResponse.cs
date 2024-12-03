namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class SensorMovimientoResponse : SensorResponse
{
    public List<RegistroMovimientoResponse> RegistrosMovimiento { get; set; } = [];
}