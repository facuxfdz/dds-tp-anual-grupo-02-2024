using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Dto.Responses.Incidentes;
using AccesoAlimentario.Operations.Dto.Responses.Sensores;

namespace AccesoAlimentario.Operations.Dto.Responses.Heladeras;

public class HeladeraResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public PuntoEstrategicoResponse PuntoEstrategico { get; set; } = null!;
    public List<ViandaResponse> Viandas { get; set; } = [];
    public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
    public DateTime FechaInstalacion { get; set; } = DateTime.UtcNow;
    public float TemperaturaActual { get; set; } = 0;
    public float TemperaturaMinimaConfig { get; set; } = 0;
    public float TemperaturaMaximaConfig { get; set; } = 0;
    public List<SensorResponse> Sensores { get; set; } = [];
    public List<IncidenteResponse> Incidentes { get; set; } = [];
    public ModeloHeladeraResponse Modelo { get; set; } = null!;
}

public class HeladeraResponseMinimo
{
    public Guid Id { get; set; } = Guid.Empty;
    public PuntoEstrategicoResponse PuntoEstrategico { get; set; } = null!;
    public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
    public DateTime FechaInstalacion { get; set; } = DateTime.UtcNow;
    public float TemperaturaActual { get; set; } = 0;
    public float TemperaturaMinimaConfig { get; set; } = 0;
    public float TemperaturaMaximaConfig { get; set; } = 0;
    public ModeloHeladeraResponse Modelo { get; set; } = null!;
}