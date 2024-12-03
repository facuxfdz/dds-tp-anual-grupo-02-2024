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
        CreateMap<Colaborador, ColaboradorResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Colaborador"));
        CreateMap<Colaborador, ColaboradorResponseExterno>()
            .ForMember(x => x.Nombre, opt => opt.MapFrom(x => x.Persona.Nombre));
            
        CreateMap<PersonaVulnerable, PersonaVulnerableResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "PersonaVulnerable"));
        
        CreateMap<Tecnico, TecnicoResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Tecnico"));
        
        CreateMap<Rol, RolResponse>()
            .Include<Colaborador, ColaboradorResponse>()
            .Include<PersonaVulnerable, PersonaVulnerableResponse>()
            .Include<Tecnico, TecnicoResponse>();
    }
}