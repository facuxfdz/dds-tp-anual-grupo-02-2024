using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Vianda
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Comida { get; set; } = null!;
    public DateTime FechaDonacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaCaducidad { get; set; } = DateTime.UtcNow;
    public virtual Colaborador? Colaborador { get; set; } = null!;
    public virtual Heladera Heladera { get; set; } = null!;
    public float Calorias { get; set; } = 0;
    public float Peso { get; set; } = 0;
    public EstadoVianda Estado { get; set; } = EstadoVianda.Disponible;
    public virtual ViandaEstandar UnidadEstandar { get; set; } = null!;

    public Vianda()
    {
    }

    public Vianda(string comida, DateTime fechaDonacion, DateTime fechaCaducidad, Colaborador colaborador,
        Heladera heladera, float calorias, float peso, EstadoVianda estado, ViandaEstandar unidadEstandar)
    {
        Comida = comida;
        FechaDonacion = fechaDonacion;
        FechaCaducidad = fechaCaducidad;
        Colaborador = colaborador;
        Heladera = heladera;
        Calorias = calorias;
        Peso = peso;
        Estado = estado;
        UnidadEstandar = unidadEstandar;
    }
}