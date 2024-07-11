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
        string descripcion = "Reporte de viandas donadas por colaborador";
        List<EntradaReporte> entradas = new List<EntradaReporte>();
        return new Reporte(descripcion, entradas);
    }
}