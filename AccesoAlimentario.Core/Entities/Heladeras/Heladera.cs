using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    public PuntoEstrategico PuntoEstrategico;
    public List<Vianda> Viandas;
    public EstadoHeladera EstadoHeladera;
    public DateTime FechaInstalacion;
    public float TemperaturaMinimaConfig { get; set; }
    public float TemperaturaMaximaConfig { get; set; }
    public List<ISensor> Sensores;
    public List<Incidente> Incidentes;
    public ModeloHeladera Modelo;

    public Heladera(PuntoEstrategico puntoEstrategico,float temperaturaMinima, float temperaturaMaxima, ModeloHeladera modelo)
    {
        PuntoEstrategico = puntoEstrategico;
        Viandas = new List<Vianda>();
        EstadoHeladera = EstadoHeladera.Activa;
        FechaInstalacion = DateTime.Now;
        TemperaturaMinimaConfig = temperaturaMinima;
        TemperaturaMaximaConfig = temperaturaMaxima;
        Sensores = new List<ISensor>();
        Incidentes = new List<Incidente>();
        Modelo = modelo;
    }
    
    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        EstadoHeladera = estadoHeladera;
    }

    public void CambioEstadoSensorTemperatura(float dato){
        if(dato >= TemperaturaMinimaConfig || dato <= TemperaturaMaximaConfig)
        { 
            //TODO cambiarlas a las del fabricante
            //TODO: aca se cambiaria el estado del sensor, pero hay una lista
        }
        else
        {
            throw new Exception("Rango de temperatura invalido");

        }
    }


    public void CambioEstadoSensorMovimiento(bool dato)
    {
        //TODO: aca se cambiaria el estado del sensor, pero hay una lista
    }

    public void AgregarSensor(ISensor sensor)
    {
        Sensores.Add(sensor);
    }
    
    public void EliminarSensor(ISensor sensor){
        Sensores.Remove(sensor);
    }

    public int ObtenerCantidadDeViandas()
    {
        return Viandas.Count;
    }

    public EstadoHeladera ObtenerEstadoHeladera()
    {
        return EstadoHeladera;
    }

    public float ObtenerLatitud()
    {
        return PuntoEstrategico.Latitud;
    }

    public float ObtenerLongitud()
    {
        return PuntoEstrategico.Longitud;
    }

}