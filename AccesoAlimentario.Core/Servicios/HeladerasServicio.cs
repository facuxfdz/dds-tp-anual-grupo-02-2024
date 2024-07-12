using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Servicios;

public class HeladerasServicio {
    public void Crear(int id, string nombre, float longi, float lat, Direccion direccion, ModeloHeladera modelo, float temperaturaMinima, float temperaturaMaxima) 
    {
        /*PuntoEstrategico puntoEstrategico = new PuntoEstrategico(id, nombre, longi, lat, direccion);
        Heladera heladera = new Heladera(puntoEstrategico, temperaturaMinima, temperaturaMaxima, modelo);*/
    }

    public void Eliminar(Heladera heladera)
    {
        //TODO
    }
    
    public void Modificar(Heladera heladera, float? temperaturaMinima, float? temperaturaMaxima)
    {
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

    public void AgregarSensor(Heladera heladera, Sensor sensor)
    {
        heladera.AgregarSensor(sensor);
    }
    
    public void EliminarSensor(Heladera heladera, Sensor sensor)
    {
        heladera.EliminarSensor(sensor);
    }
    
    public EstadoHeladera ObtenerEstadoHeladera(Heladera heladera)
    {
        return heladera.ObtenerEstadoHeladera();
    }
    
}