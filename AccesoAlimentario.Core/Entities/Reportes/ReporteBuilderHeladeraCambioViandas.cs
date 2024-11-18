using System.Text.Json;
using AccesoAlimentario.Core.DAL;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraCambioViandas : IReporteBuilder
{
    public IUnitOfWork UnitOfWork { get; }
    
    public ReporteBuilderHeladeraCambioViandas(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    
    public async Task<Reporte> Generar(DateTime fechaInicio, DateTime fechaFin)
    {
        var reporte = new Reporte();
        var distribucionViandasQuery = UnitOfWork.DistribucionViandasRepository.GetQueryable();
        var donacionViandasQuery = UnitOfWork.DonacionViandaRepository.GetQueryable();
        
        distribucionViandasQuery = distribucionViandasQuery.Where(d => d.FechaContribucion >= fechaInicio && d.FechaContribucion <= fechaFin);
        donacionViandasQuery = donacionViandasQuery.Where(d => d.FechaContribucion >= fechaInicio && d.FechaContribucion <= fechaFin);
        
        var distribucionViandas = await UnitOfWork.DistribucionViandasRepository.GetCollectionAsync(distribucionViandasQuery);
        var donacionViandas = await UnitOfWork.DonacionViandaRepository.GetCollectionAsync(donacionViandasQuery);
        
        // TODO: Implementar lógica para generar el reporte
        
        reporte.FechaCreacion = DateTime.Now;
        reporte.FechaExpiracion = fechaFin;
        reporte.Cuerpo = JsonSerializer.Serialize(new List<string>());
        reporte.Tipo = TipoReporte.CANTIDAD_VIANDAS_RET_COL_POR_HELADERA;
        
        return reporte;
    }
}