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
        var heladerasQuery = UnitOfWork.HeladeraRepository.GetQueryable();
        var heladeras = await UnitOfWork.HeladeraRepository.GetCollectionAsync(heladerasQuery);

        var reporteHeladeras = new List<object>();
        foreach (var heladera in heladeras)
        {
            var distribucionViandasQuery = UnitOfWork.DistribucionViandasRepository.GetQueryable();
            var donacionViandasQuery = UnitOfWork.DonacionViandaRepository.GetQueryable();
            distribucionViandasQuery = distribucionViandasQuery
                .Where(d => d.FechaContribucion >= fechaInicio && d.FechaContribucion <= fechaFin)
                .Where(d =>
                    (d.HeladeraOrigen != null && d.HeladeraOrigen.Id == heladera.Id) ||
                    (d.HeladeraDestino != null && d.HeladeraDestino.Id == heladera.Id));
            donacionViandasQuery =
                donacionViandasQuery
                    .Where(d => d.FechaContribucion >= fechaInicio && d.FechaContribucion <= fechaFin)
                    .Where(d => d.Heladera != null && d.Heladera.Id == heladera.Id);
            
            var distribucionViandas = await UnitOfWork.DistribucionViandasRepository.GetCollectionAsync(distribucionViandasQuery);
            var donacionViandas = await UnitOfWork.DonacionViandaRepository.GetCollectionAsync(donacionViandasQuery);
            
            var cantidadViandasColocadas = donacionViandas.Count();
            var cantidadVindasRetiradas = 0;

            foreach (var distribucion in distribucionViandas)
            {
                if (distribucion.HeladeraOrigen != null && distribucion.HeladeraOrigen.Id == heladera.Id)
                {
                    cantidadVindasRetiradas += distribucion.CantViandas;
                } else if (distribucion.HeladeraDestino != null && distribucion.HeladeraDestino.Id == heladera.Id)
                {
                    cantidadViandasColocadas += distribucion.CantViandas;
                }
            }
            
            var reporteHeladera = new
            {
                Heladera = heladera.PuntoEstrategico.Nombre,
                CantidadViandasColocadas = cantidadViandasColocadas,
                CantidadViandasRetiradas = cantidadVindasRetiradas
            };
            
            reporteHeladeras.Add(reporteHeladera);
        }

        reporte.FechaCreacion = DateTime.Now;
        reporte.FechaExpiracion = fechaFin;
        reporte.Cuerpo = JsonSerializer.Serialize(reporteHeladeras);
        reporte.Tipo = TipoReporte.CANTIDAD_VIANDAS_RET_COL_POR_HELADERA;

        return reporte;
    }
}