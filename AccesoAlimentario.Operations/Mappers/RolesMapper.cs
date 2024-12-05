using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
using AccesoAlimentario.Operations.Dto.Responses.Roles;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class RolesMapper : Profile
{
    public RolesMapper()
    {
        CreateMap<AreaCobertura, AreaCoberturaResponse>();
        
        CreateMap<Colaborador, ColaboradorResponseExterno>()
            .ForMember(x => x.Nombre, opt => opt.MapFrom(x => x.Persona.Nombre));
        
        CreateMap<Colaborador, ColaboradorResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Colaborador"));
        CreateMap<Colaborador, ColaboradorResponseMinimo>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Colaborador"));
        
        CreateMap<PersonaVulnerable, PersonaVulnerableResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "PersonaVulnerable"));
        CreateMap<PersonaVulnerable, PersonaVulnerableResponseMinimo>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "PersonaVulnerable"));
        
        CreateMap<Tecnico, TecnicoResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Tecnico"));
        CreateMap<Tecnico, TecnicoResponseMinimo>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Tecnico"));
        
        CreateMap<Rol, RolResponse>()
            .Include<Colaborador, ColaboradorResponse>()
            .Include<PersonaVulnerable, PersonaVulnerableResponse>()
            .Include<Tecnico, TecnicoResponse>();
        
        CreateMap<Rol, RolResponseMinimo>()
            .Include<Colaborador, ColaboradorResponseMinimo>()
            .Include<PersonaVulnerable, PersonaVulnerableResponseMinimo>()
            .Include<Tecnico, TecnicoResponseMinimo>();
    }
}