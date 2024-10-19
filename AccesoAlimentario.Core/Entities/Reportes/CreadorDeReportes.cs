using AccesoAlimentario.Core.DAL;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class CreadorDeReportes
{
    public List<Reporte> ReportesVigentes { get; set; } = [];
    public List<Reporte> ReportesHistoricos { get; set; } = [];

    public async void GenerarReportes(IUnitOfWork unitOfWork)
    {
        var today = DateTime.Today;
        var currentDay = today.DayOfWeek;
        var daysSinceLastSunday = (int)currentDay + 1;
        var endOfLastWeek = today.AddDays(-daysSinceLastSunday);
        var startOfLastWeek = endOfLastWeek.AddDays(-6);

        List<IReporteBuilder> conceptos =
        [
            new ReporteBuilderHeladeraFallas(unitOfWork),
            new ReporteBuilderColaboradorViandasDonadas(unitOfWork),
            new ReporteBuilderHeladeraCambioViandas(unitOfWork),
        ];

        ReportesVigentes.Clear();

        foreach (var concepto in conceptos)
        {
            var reporte = await concepto.Generar(startOfLastWeek, endOfLastWeek);
            ReportesVigentes.Add(reporte);
        }

        ReportesVigentes.AddRange(ReportesHistoricos);
    }

    public Reporte ObtenerReporteVigente(TipoReporte tipo)
    {
        return ReportesVigentes.FirstOrDefault(r => r.Tipo == tipo)!;
    }
}