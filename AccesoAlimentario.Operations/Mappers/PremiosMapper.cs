using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Operations.Dto.Responses.Premios;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class PremiosMapper : Profile
{
    public PremiosMapper()
    {
        CreateMap<Premio, PremioResponse>();
    }
}