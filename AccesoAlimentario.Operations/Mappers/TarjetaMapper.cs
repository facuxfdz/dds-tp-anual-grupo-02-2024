using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AccesoAlimentario.Operations.Dto.Responses.Tarjetas;
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
        
        CreateMap<TarjetaColaboracion, TarjetaColaboracionResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Colaboracion"));
        
        CreateMap<TarjetaConsumo, TarjetaConsumoResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Consumo"));
        
        CreateMap<Tarjeta, TarjetaResponse>()
            .Include<TarjetaColaboracion, TarjetaColaboracionResponse>()
            .Include<TarjetaConsumo, TarjetaConsumoResponse>();
    }
}