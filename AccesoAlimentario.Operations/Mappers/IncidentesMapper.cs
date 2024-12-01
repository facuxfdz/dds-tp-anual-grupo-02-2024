using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Operations.Dto.Responses.Incidentes;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class IncidentesMapper : Profile
{
    public IncidentesMapper()
    {
        CreateMap<Alerta, AlertaResponse>()
            .ForMember(x => x.TipoIncidente, opt => opt.MapFrom(x => "Alerta"));
        
        CreateMap<FallaTecnica, FallaTecnicaResponse>()
            .ForMember(x => x.TipoIncidente, opt => opt.MapFrom(x => "FallaTecnica"));

        CreateMap<VisitaTecnica, VisitaTecnicaResponse>();
        
        CreateMap<Incidente, IncidenteResponse>()
            .Include<Alerta, AlertaResponse>()
            .Include<FallaTecnica, FallaTecnicaResponse>();
    }
}