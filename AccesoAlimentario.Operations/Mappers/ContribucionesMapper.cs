using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Operations.Dto.Responses.Contribuciones;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class ContribucionesMapper : Profile
{
    public ContribucionesMapper()
    {
        CreateMap<AdministracionHeladera, AdministracionHeladeraResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "AdministracionHeladera"));
        
        CreateMap<DistribucionViandas, DistribucionViandasResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "DistribucionViandas"));
        
        CreateMap<DonacionMonetaria, DonacionMonetariaResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "DonacionMonetaria"));
        
        CreateMap<DonacionVianda, DonacionViandaResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "DonacionVianda"));
        
        CreateMap<OfertaPremio, OfertaPremioResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "OfertaPremio"));
        
        CreateMap<RegistroPersonaVulnerable, RegistroPersonaVulnerableResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "RegistroPersonaVulnerable"));
        
        CreateMap<FormaContribucion, FormaContribucionResponse>()
            .Include<AdministracionHeladera, AdministracionHeladeraResponse>()
            .Include<DistribucionViandas, DistribucionViandasResponse>()
            .Include<DonacionMonetaria, DonacionMonetariaResponse>()
            .Include<DonacionVianda, DonacionViandaResponse>()
            .Include<OfertaPremio, OfertaPremioResponse>()
            .Include<RegistroPersonaVulnerable, RegistroPersonaVulnerableResponse>();
    }
}