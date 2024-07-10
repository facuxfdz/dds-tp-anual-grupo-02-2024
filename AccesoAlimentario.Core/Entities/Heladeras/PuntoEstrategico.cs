using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class PuntoEstrategico
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public float Longitud { get; private set; }
    public float Latitud { get; private set; }
    public Direccion Direccion { get; private set; }
    
    public PuntoEstrategico()
    {
    }

    public PuntoEstrategico(int id, string nombre, float longitud, float latitud, Direccion direccion)
    {
        Id = id;
        Nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        Direccion = direccion;
    }

    public void Actualizar(string nombre, float longitud, float latitud, Direccion direccion)
    {
        Nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        Direccion = direccion;
    }
}