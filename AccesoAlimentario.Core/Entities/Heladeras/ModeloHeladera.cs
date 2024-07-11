public class ModeloHeladera
{
    private int Capacidad { get; set; }
    private int TemperaturaMinima { get; set; }
    private int TemperaturaMaxima { get; set; }

    public ModeloHeladera(int capacidad, int temperaturaMinima, int temperaturaMaxima)
    {
        Capacidad = capacidad;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }
}