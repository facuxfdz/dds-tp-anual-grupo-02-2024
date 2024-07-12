using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Vianda
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Comida { get; private set; } = null!;
    public DateTime FechaDonacion { get; private set; } = DateTime.Now;
    public DateTime FechaCaducidad { get; private set; } = DateTime.Now;
    public Colaborador Colaborador { get; private set; } = null!;
    public Heladera Heladera { get; private set; } = null!;
    public float Calorias { get; private set; } = 0;
    public float Peso { get; private set; } = 0;
    public EstadoVianda EstadoVianda { get; private set; } = EstadoVianda.Disponible;
    public ViandaEstandar UnidadEstandar { get; private set; } = null!;

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
}