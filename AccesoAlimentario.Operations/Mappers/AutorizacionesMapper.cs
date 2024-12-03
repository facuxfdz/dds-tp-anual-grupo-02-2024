using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class AutorizacionesMapper : Profile
{
    public AutorizacionesMapper()
    {
        CreateMap<AccesoHeladera, AccesoHeladeraResponse>();
        CreateMap<AutorizacionManipulacionHeladera, AutorizacionManipulacionHeladeraResponse>();
    }
}