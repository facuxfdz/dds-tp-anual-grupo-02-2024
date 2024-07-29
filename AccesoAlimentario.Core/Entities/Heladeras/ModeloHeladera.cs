using System.ComponentModel.DataAnnotations;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ModeloHeladera
{
    [Key]
    public string Id { get; private set; }

    public float Capacidad { get; set; }
    public float TemperaturaMinima { get; set; }
    public float TemperaturaMaxima { get; set; }
    
    public ModeloHeladera()
    {
    }

    public ModeloHeladera(string id, float capacidad, float temperaturaMinima, float temperaturaMaxima)
    {
        Id = id;
        Capacidad = capacidad;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }
}