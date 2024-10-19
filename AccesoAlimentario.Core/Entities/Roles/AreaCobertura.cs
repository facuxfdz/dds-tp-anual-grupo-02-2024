namespace AccesoAlimentario.Core.Entities.Roles;

public class AreaCobertura
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public float Latitud { get; set; } = 0f;
    public float Longitud { get; set; } = 0f;
    public float Radio { get; set; } = 0f;

    public AreaCobertura()
    {
    }

    public AreaCobertura(float latitud, float longitud, float radio)
    {
        Latitud = latitud;
        Longitud = longitud;
        Radio = radio;
    }

    public void ActualizarArea(float latitud, float longitud, float radio)
    {
        Latitud = latitud;
        Longitud = longitud;
        Radio = radio;
    }

    public bool EsCercano(float longitud, float latitud)
    {
        var distancia = Math.Sqrt(Math.Pow(longitud - Longitud, 2) + Math.Pow(latitud - Latitud, 2));
        return distancia <= Radio;
    }
}