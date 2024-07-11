using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Contribuciones;


namespace AccesoAlimentario.Core.Entities.Reportes;

public interface IReporteBuilder
{
    public Reporte Generar(List<Heladera> heladera, List<Incidente> incidentes, List<FormaContribucion> contribuciones);
}