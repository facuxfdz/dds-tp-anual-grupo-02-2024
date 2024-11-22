using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class HeladeraMapper : Profile
{
    public HeladeraMapper()
    {
        CreateMap<PuntoEstrategicoRequest, PuntoEstrategico>();
        CreateMap<SensorTemperaturaRequest, SensorTemperatura>();
        CreateMap<SensorMovimientoRequest, SensorMovimiento>();
        CreateMap<ModeloHeladeraRequest, ModeloHeladera>();
        CreateMap<SensorRequest, Sensor>()
            .Include<SensorTemperaturaRequest, SensorTemperatura>()
            .Include<SensorMovimientoRequest, SensorMovimiento>();
    }
}