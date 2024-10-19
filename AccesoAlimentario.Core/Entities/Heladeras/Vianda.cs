using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Vianda
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Comida { get; set; } = null!;
    public DateTime FechaDonacion { get; set; } = DateTime.Now;
    public DateTime FechaCaducidad { get; set; } = DateTime.Now;
    public Colaborador Colaborador { get; set; } = null!;
    public Heladera Heladera { get; set; } = null!;
    public float Calorias { get; set; } = 0;
    public float Peso { get; set; } = 0;
    public EstadoVianda EstadoVianda { get; set; } = EstadoVianda.Disponible;
    public ViandaEstandar UnidadEstandar { get; set; } = null!;

    public Vianda()
    {
    }

    public Vianda(string comida, DateTime fechaDonacion, DateTime fechaCaducidad, Colaborador colaborador,
        Heladera heladera, float calorias, float peso, EstadoVianda estadoVianda, ViandaEstandar unidadEstandar)
    {
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
}