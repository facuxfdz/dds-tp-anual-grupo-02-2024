namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ModeloHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

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