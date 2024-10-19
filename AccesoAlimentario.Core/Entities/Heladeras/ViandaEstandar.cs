namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ViandaEstandar
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public float Largo { get; set; } = 0;
    public float Ancho { get; set; } = 0;
    public float Profundidad { get; set; } = 0;

    public ViandaEstandar()
    {
    }

    public ViandaEstandar(float largo, float ancho, float profundidad)
    {
        Largo = largo;
        Ancho = ancho;
        Profundidad = profundidad;
    }

    public void Actualizar(float largo, float ancho, float profundidad)
    {
        Largo = largo;
        Ancho = ancho;
        Profundidad = profundidad;
    }
}