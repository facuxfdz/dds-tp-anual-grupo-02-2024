using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Responses.Direcciones;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class DireccionesMapper : Profile
{
    public DireccionesMapper()
    {
        CreateMap<DireccionRequest, Direccion>();
        CreateMap<Direccion, DireccionResponse>();
    }
}