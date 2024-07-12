using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderColaboradorViandasDonadas : IReporteBuilder
{
    public ReporteBuilderColaboradorViandasDonadas()
    {
    }

    public Reporte Generar(List<Heladera> heladera, List<Incidente> incidentes, List<FormaContribucion> contribuciones)
    {
        var descripcion = "Reporte de viandas donadas por colaborador";
        List<EntradaReporte> entradas = [];
        return new Reporte(descripcion, entradas);
    }
}