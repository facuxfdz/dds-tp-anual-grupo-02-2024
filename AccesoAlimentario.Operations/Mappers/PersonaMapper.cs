using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
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
    }}