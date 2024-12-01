using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Responses.Personas;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class PersonaMapper : Profile
{
    public PersonaMapper()
    {
        CreateMap<PersonaRequest, Persona>()
            .Include<PersonaHumanaRequest, PersonaHumana>()
            .Include<PersonaJuridicaRequest, PersonaJuridica>();
        
        CreateMap<PersonaHumanaRequest, PersonaHumana>();
        CreateMap<PersonaJuridicaRequest, PersonaJuridica>();
        
        CreateMap<PersonaHumana, PersonaHumanaResponse>()
            .ForMember(x => x.TipoPersona, opt => opt.MapFrom(x => "Humana"));
        
        CreateMap<PersonaJuridica, PersonaJuridicaResponse>()
            .ForMember(x => x.TipoPersona, opt => opt.MapFrom(x => "Juridica"));
        
        CreateMap<Persona, PersonaResponse>()
            .Include<PersonaHumana, PersonaHumanaResponse>()
            .Include<PersonaJuridica, PersonaJuridicaResponse>();
    }}