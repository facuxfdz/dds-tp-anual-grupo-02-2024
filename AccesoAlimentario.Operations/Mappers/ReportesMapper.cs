using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Dto.Responses.Reportes;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class ReportesMapper : Profile
{
    public ReportesMapper()
    {
        CreateMap<Reporte, ReporteResponse>();
    }
}