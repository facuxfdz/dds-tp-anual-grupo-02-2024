using System.Text.Json;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderColaboradorViandasDonadas : IReporteBuilder
{
    public IUnitOfWork UnitOfWork { get; }

    public ReporteBuilderColaboradorViandasDonadas(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Reporte> Generar(DateTime fechaInicio, DateTime fechaFin)
    {
        var reporte = new Reporte();
        var colaboradoresQuery = UnitOfWork.ColaboradorRepository.GetQueryable();
        var colaboradores = await UnitOfWork.ColaboradorRepository.GetCollectionAsync(colaboradoresQuery);

        var reporteColaboradores = new List<object>();

        foreach (var colaborador in colaboradores)
        {
            var viandasDonadas = colaborador.ContribucionesRealizadas.OfType<DonacionVianda>()
                .Where(v => v.FechaContribucion >= fechaInicio && v.FechaContribucion <= fechaFin);
            var cantidadViandas = viandasDonadas.Count();
            if (cantidadViandas == 0)
            {
                continue;
            }

            var reporteColaborador = new
            {
                Colaborador = colaborador.Persona.Nombre,
                CantidadViandas = cantidadViandas
            };
            reporteColaboradores.Add(reporteColaborador);
        }

        reporte.FechaCreacion = DateTime.Now;
        reporte.FechaExpiracion = fechaFin;
        reporte.Cuerpo = JsonSerializer.Serialize(reporteColaboradores);
        reporte.Tipo = TipoReporte.CANTIDAD_VIANDAS_POR_COLABORADOR;

        return reporte;
    }
}