using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class HeladeraMapper : Profile
{
    public HeladeraMapper()
    {
        CreateMap<PuntoEstrategicoRequest, PuntoEstrategico>();
        CreateMap<ModeloHeladeraRequest, ModeloHeladera>();

        CreateMap<Heladera, HeladeraResponse>();
        CreateMap<ModeloHeladera, ModeloHeladeraResponse>();
        CreateMap<PuntoEstrategico, PuntoEstrategicoResponse>();
        CreateMap<Vianda, ViandaResponse>();
    }
}