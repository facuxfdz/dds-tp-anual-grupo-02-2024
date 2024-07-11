namespace AccesoAlimentario.Core.Entities.Personas.Tecnicos;

public class AreaCobertura
{
    private float _latitud;
    private float _longitud;
    private float _radio;
    
    public AreaCobertura(float latitud, float longitud, float radio)
    {
        _latitud = latitud;
        _longitud = longitud;
        _radio = radio;
    }
    
    public void ActualizarArea(float latitud, float longitud, float radio)
    {
        _latitud = latitud;
        _longitud = longitud;
        _radio = radio;
    }

    public bool EsCercano(float longitud, float latitud)
    {
        var distancia = Math.Sqrt(Math.Pow(longitud - _longitud, 2) + Math.Pow(latitud - _latitud, 2));
        return distancia <= _radio;
    }
}