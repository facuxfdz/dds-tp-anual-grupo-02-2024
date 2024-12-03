using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AccesoAlimentario.Operations.Dto.Responses.Sensores;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class SensoresMapper : Profile
{
    public SensoresMapper()
    {
        CreateMap<RegistroMovimiento, RegistroMovimientoResponse>();
        CreateMap<RegistroTemperatura, RegistroTemperaturaResponse>();
        
        CreateMap<SensorTemperaturaRequest, SensorTemperatura>();
        CreateMap<SensorMovimientoRequest, SensorMovimiento>();
        CreateMap<SensorRequest, Sensor>()
            .Include<SensorTemperaturaRequest, SensorTemperatura>()
            .Include<SensorMovimientoRequest, SensorMovimiento>();
        
        CreateMap<SensorTemperatura, SensorTemperaturaResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Temperatura"));
        
        CreateMap<SensorMovimiento, SensorMovimientoResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Movimiento"));

        CreateMap<Sensor, SensorResponse>()
            .Include<SensorTemperatura, SensorTemperaturaResponse>()
            .Include<SensorMovimiento, SensorMovimientoResponse>();
    }
}