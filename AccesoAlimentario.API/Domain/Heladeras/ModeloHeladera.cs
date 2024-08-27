using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Heladeras;

public class ModeloHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; private set; }

    public float Capacidad { get; set; }
    public float TemperaturaMinima { get; set; }
    public float TemperaturaMaxima { get; set; }
    
    public ModeloHeladera()
    {
    }

    public ModeloHeladera(float capacidad, float temperaturaMinima, float temperaturaMaxima)
    {
        Capacidad = capacidad;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }
}