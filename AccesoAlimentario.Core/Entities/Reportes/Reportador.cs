namespace AccesoAlimentario.Core.Entities.Reportes

public class Reportador{
    private List<Heladera> _heladeras;
    private List<Incidente> _incidentes;
    private List<FormaContribucion> _formasContribucion;

    public Reportador(List<Heladera> heladeras, List<Incidente> incidentes, List<FormaContribucion> formasContribucion){
        _heladeras = heladeras;
        _incidentes = incidentes;
        _formasContribucion = formasContribucion;
    }
}