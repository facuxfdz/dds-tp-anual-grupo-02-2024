using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class TarjetaMapper : Profile
{
    public TarjetaMapper()
    {
        CreateMap<TarjetaRequest, Tarjeta>()
            .Include<TarjetaColaboracionRequest, TarjetaColaboracion>()
            .Include<TarjetaConsumoRequest, TarjetaConsumo>();
        
        CreateMap<TarjetaColaboracionRequest, TarjetaColaboracion>();
        CreateMap<TarjetaConsumoRequest, TarjetaConsumo>();
    }
}