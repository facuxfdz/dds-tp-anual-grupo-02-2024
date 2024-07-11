namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ViandaEstandar
{
    public int Id { get; private set; }
    public float Largo { get; private set; }
    public float Ancho { get; private set; }
    public float Profundidad { get; private set; }

    public ViandaEstandar()
    {
    }

    public ViandaEstandar(int id, float largo, float ancho, float profundidad)
    {
        Id = id;
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