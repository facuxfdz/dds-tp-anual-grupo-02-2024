using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class DireccionMapper : Profile
{
    public DireccionMapper()
    {
        CreateMap<DireccionRequest, Direccion>();
    }
}