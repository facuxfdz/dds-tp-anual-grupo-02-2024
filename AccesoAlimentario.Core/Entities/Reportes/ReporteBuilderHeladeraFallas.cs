using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Reportes;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraFallas : IReporteBuilder{
    private int _cantidadFallas;
    
    public ReporteBuilderHeladeraFallas(int cantidadFallas)
    {
        _cantidadFallas = cantidadFallas;
    }
    
    public Reporte Generar(List<Heladera> heladera, List<Incidente> incidentes, List<FormaContribucion> contribuciones)
    {
        string descripcion = "Reporte de fallas en heladera";
        List<EntradaReporte> entradas = new List<EntradaReporte>();
        return new Reporte(descripcion, entradas);
    }
}