using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ModeloHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    private int Capacidad { get; set; } = 0;
    private int TemperaturaMinima { get; set; } = 0;
    private int TemperaturaMaxima { get; set; } = 0;
    
    public ModeloHeladera()
    {
    }

    public ModeloHeladera(int capacidad, int temperaturaMinima, int temperaturaMaxima)
    {
        Capacidad = capacidad;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }
}