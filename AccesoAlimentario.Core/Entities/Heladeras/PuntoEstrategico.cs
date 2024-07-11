using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class PuntoEstrategico
{
    public string _nombre {get; private set;}
    public float Longitud {get; private set;}
    public float Latitud {get; private set;}
    private Direccion _direccion;

    public PuntoEstrategico(string nombre, float longitud, float latitud, Direccion direccion)
    {
        _nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        _direccion = direccion;
    }

    public void Actualizar(string nombre, float longitud, float latitud, Direccion direccion)
    {
        _nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        _direccion = direccion;
    }
}