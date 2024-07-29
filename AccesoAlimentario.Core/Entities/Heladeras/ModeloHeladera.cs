using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ModeloHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; private set; }

    public int Capacidad { get; set; } = 0;
    public int TemperaturaMinima { get; set; } = 0;
    public int TemperaturaMaxima { get; set; } = 0;
    
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