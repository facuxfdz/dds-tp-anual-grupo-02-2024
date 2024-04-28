using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class PuntoEstrategico
{
    private string _nombre;
    private float _longitud;
    private float _latitud;
    private Direccion _direccion;

    public PuntoEstrategico(string nombre, float longitud, float latitud, Direccion direccion)
    {
        _nombre = nombre;
        _longitud = longitud;
        _latitud = latitud;
        _direccion = direccion;
    }
    
    public void Actualizar(string nombre, float longitud, float latitud, Direccion direccion)
    {
        _nombre = nombre;
        _longitud = longitud;
        _latitud = latitud;
        _direccion = direccion;
    }

}