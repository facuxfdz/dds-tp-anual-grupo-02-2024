namespace AccesoAlimentario.Core.Entities.Personas;

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
}