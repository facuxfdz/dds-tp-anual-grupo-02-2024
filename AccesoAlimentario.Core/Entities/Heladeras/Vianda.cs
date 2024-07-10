using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Vianda
{
    public int Id { get; private set; }
    public string Comida { get; private set; }
    public DateTime FechaDonacion { get; private set; }
    public DateTime FechaCaducidad { get; private set; }
    public Colaborador Colaborador { get; private set; }
    public Heladera Heladera { get; private set; }
    public float Calorias { get; private set; }
    public float Peso { get; private set; }
    public EstadoVianda EstadoVianda { get; private set; }
    public ViandaEstandar UnidadEstandar { get; private set; }

    public Vianda()
    {
    }

    public Vianda(int id, string comida, DateTime fechaDonacion, DateTime fechaCaducidad, Colaborador colaborador,
        Heladera heladera, float calorias, float peso, EstadoVianda estadoVianda, ViandaEstandar unidadEstandar)
    {
        Id = id;
        Comida = comida;
        FechaDonacion = fechaDonacion;
        FechaCaducidad = fechaCaducidad;
        Colaborador = colaborador;
        Heladera = heladera;
        Calorias = calorias;
        Peso = peso;
        EstadoVianda = estadoVianda;
        UnidadEstandar = unidadEstandar;
    }

    public void ActualizarHeladera(Heladera heladera)
    {
        Heladera = heladera;
    }
}