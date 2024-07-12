using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraCambioViandas : IReporteBuilder
{
    public ReporteBuilderHeladeraCambioViandas()
    {
    }

    public Reporte Generar(List<Heladera> heladera, List<Incidente> incidentes, List<FormaContribucion> contribuciones)
    {
        var descripcion = "Reporte de cambio de viandas en heladera";
        List<EntradaReporte> entradas = [];
        return new Reporte(descripcion, entradas);
    }
}