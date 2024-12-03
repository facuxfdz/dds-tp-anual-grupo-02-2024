using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Operations.Dto.Responses.SuscripcionesColaboradores;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class SuscripcionesMapper : Profile
{
    public SuscripcionesMapper()
    {
        CreateMap<SuscripcionExcedenteViandas, SuscripcionExcedenteViandasResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "ExcedenteViandas"));
        
        CreateMap<SuscripcionFaltanteViandas, SuscripcionFaltanteViandasResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "FaltanteViandas"));
        
        CreateMap<SuscripcionIncidenteHeladera, SuscripcionIncidenteHeladeraResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "IncidenteHeladera"));
        
        CreateMap<Suscripcion, SuscripcionResponse>()
            .Include<SuscripcionExcedenteViandas, SuscripcionExcedenteViandasResponse>()
            .Include<SuscripcionFaltanteViandas, SuscripcionFaltanteViandasResponse>()
            .Include<SuscripcionIncidenteHeladera, SuscripcionIncidenteHeladeraResponse>();
    }
}