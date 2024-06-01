namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    private PuntoEstrategico _puntoEstrategico;
    private int _capacidad;
    private List<Vianda> _viandas;
    private EstadoHeladera _estadoHeladera;
    private DateTime _fechaInstalacion;
    private float _temperaturaMinima;
    private float _temperaturaMaxima;

    public Heladera(PuntoEstrategico puntoEstrategico, int capacidad, List<Vianda> viandas,
        EstadoHeladera estadoHeladera, DateTime fechaInstalacion, float temperaturaMinima, float temperaturaMaxima)
    {
        _puntoEstrategico = puntoEstrategico;
        _capacidad = capacidad;
        _viandas = viandas;
        _estadoHeladera = estadoHeladera;
        _fechaInstalacion = fechaInstalacion;
        _temperaturaMinima = temperaturaMinima;
        _temperaturaMaxima = temperaturaMaxima;
    }

    public void IngresarViandas(Heladera heladeraOrigen, List<Vianda> viandas)
    {
        if (_estadoHeladera == EstadoHeladera.Funcionando)
        {
            if (_viandas.Count + viandas.Count <= _capacidad)
            {
                _viandas.AddRange(viandas);
            }
            else
            {
                throw new Exception("No hay espacio suficiente en la heladera");
            }
        }
        else
        {
            throw new Exception("La heladera no está funcionando");
        }
    }

    public void RetirarViandas(int cantidad)
    {
        throw new NotImplementedException();
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        _estadoHeladera = estadoHeladera;
    }

    public void VerificarTemperatura(float temperatura)
    {
        throw new NotImplementedException();
    }

    public void ActualizarRangoTemperatura(float temperaturaMinima, float temperaturaMaxima)
    {
        _temperaturaMinima = temperaturaMinima;
        _temperaturaMaxima = temperaturaMaxima;
    }
    
    public EstadoHeladera EstadoHeladera => _estadoHeladera;
}