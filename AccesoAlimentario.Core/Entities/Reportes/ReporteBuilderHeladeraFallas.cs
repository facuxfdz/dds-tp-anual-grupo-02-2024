using System.Text.Json;
using AccesoAlimentario.Core.DAL;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraFallas : IReporteBuilder
{
    public IUnitOfWork UnitOfWork { get; }
    
    public ReporteBuilderHeladeraFallas(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    
    public async Task<Reporte> Generar(DateTime fechaInicio, DateTime fechaFin)
    {
        var reporte = new Reporte();
        var heladerasQuery = UnitOfWork.HeladeraRepository.GetQueryable();
        heladerasQuery = heladerasQuery.Where(h => h.Incidentes.Any(i => i.Fecha >= fechaInicio && i.Fecha <= fechaFin));
        var heladeras = await UnitOfWork.HeladeraRepository.GetCollectionAsync(heladerasQuery);
        var reporteHeladeras = new List<object>();
        
        foreach (var heladera in heladeras)
        {
            var fallasHeladera = heladera.Incidentes.Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin);
            var cantidadFallas = fallasHeladera.Count();
            var reporteHeladera = new 
            {
                Heladera = heladera.PuntoEstrategico.Nombre,
                CantidadFallas = cantidadFallas
            };
            reporteHeladeras.Add(reporteHeladera);
        }
        
        reporte.FechaCreacion = DateTime.Now;
        reporte.FechaExpiracion = DateTime.Now.AddDays(7);
        reporte.Cuerpo = JsonSerializer.Serialize(reporteHeladeras);
        reporte.Tipo = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA;
        
        return reporte;
    }
}