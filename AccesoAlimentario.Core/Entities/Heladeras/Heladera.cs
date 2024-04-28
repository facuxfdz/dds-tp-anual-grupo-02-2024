namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    private PuntoEstrategico _puntoEstrategico;
    private int _capacidad;
    private List<Vianda> _viandas;
    private EstadoHeladera _estadoHeladera;
    private DateTime _fechaInstalacion;

    public Heladera(PuntoEstrategico puntoEstrategico, int capacidad, List<Vianda> viandas,
        EstadoHeladera estadoHeladera, DateTime fechaInstalacion)
    {
        _puntoEstrategico = puntoEstrategico;
        _capacidad = capacidad;
        _viandas = viandas;
        _estadoHeladera = estadoHeladera;
        _fechaInstalacion = fechaInstalacion;
    }

    public void IngresarViandas(Heladera heladeraOrigen, List<Vianda> viandas)
    {
        
    }

    public void RetirarViandas(int cantidad){
        
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        _estadoHeladera = estadoHeladera;
    }
}