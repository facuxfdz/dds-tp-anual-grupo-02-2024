using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Servicios;

public class HeladerasServicio {
    public void Crear(string nombre, float longi, float lat, Direccion direccion, ModeloHeladera modelo, float temperaturaMinima, float temperaturaMaxima) 
    {
        PuntoEstrategico puntoEstrategico = new PuntoEstrategico(nombre, longi, lat, direccion);
        Heladera heladera = new Heladera(puntoEstrategico, temperaturaMinima, temperaturaMaxima, modelo);
    }

    public void Eliminar(Heladera heladera)
    {
        //TODO
    }
    
    public void Modificar(Heladera heladera, string? nombre, float? temperaturaMinima, float? temperaturaMaxima)
    {
        if (nombre != null)
            heladera.Nombre(nombre);

        if (temperaturaMinima != null)
            heladera.TemperaturaMinimaConfig = temperaturaMinima.Value;

        if (temperaturaMaxima.HasValue)
            heladera.TemperaturaMaximaConfig = temperaturaMaxima.Value;
    }

    public Heladera Buscar()
    {
        //TODO
        return null;
    }

    public void AgregarSensor(Heladera heladera, ISensor sensor)
    {
        heladera.AgregarSensor(sensor);
    }
    
    public void EliminarSensor(Heladera heladera, ISensor sensor)
    {
        heladera.EliminarSensor(sensor);
    }
    
    public EstadoHeladera ObtenerEstadoHeladera(Heladera heladera)
    {
        return heladera.ObtenerEstadoHeladera();
    }
    
}