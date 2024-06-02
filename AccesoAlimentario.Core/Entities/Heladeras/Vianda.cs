using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Vianda
{
    private string _comida;
    private DateTime _fechaDonacion;
    private DateTime _fechaCaducidad;
    private Colaborador _colaborador;
    private Heladera _heladera;
    private float _calorias;
    private float _peso;
    private EstadoVianda _estadoVianda;
    private ViandaEstandar _unidadEstandar;

    public Vianda(string comida, DateTime fechaDonacion, DateTime fechaCaducidad, Colaborador colaborador,
        Heladera heladera, float calorias, float peso, EstadoVianda estadoVianda, ViandaEstandar unidadEstandar)
    {
        _comida = comida;
        _fechaDonacion = fechaDonacion;
        _fechaCaducidad = fechaCaducidad;
        _colaborador = colaborador;
        _heladera = heladera;
        _calorias = calorias;
        _peso = peso;
        _estadoVianda = estadoVianda;
        _unidadEstandar = unidadEstandar;
    }

    public void ActualizarHeladera(Heladera heladera)
    {
        _heladera = heladera;
    }
}