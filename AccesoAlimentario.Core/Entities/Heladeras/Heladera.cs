using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public PuntoEstrategico PuntoEstrategico { get; set; } = null!;
    public List<Vianda> Viandas { get; set; } = null!;
    public EstadoHeladera EstadoHeladera { get; set; } = EstadoHeladera.FueraServicio;
    public DateTime FechaInstalacion { get; set; } = DateTime.Now;
    public float TemperaturaMinimaConfig { get; set; } = 0;
    public float TemperaturaMaximaConfig { get; set; } = 0;
    public List<Sensor> Sensores { get; set; } = [];
    public List<Incidente> Incidentes { get; set; } = [];
    public ModeloHeladera Modelo { get; set; } = null!;

    public Heladera()
    {
    }

    public Heladera(PuntoEstrategico puntoEstrategico, float temperaturaMinima, float temperaturaMaxima,
        ModeloHeladera modelo)
    {
        PuntoEstrategico = puntoEstrategico;
        EstadoHeladera = EstadoHeladera.Activa;
        FechaInstalacion = DateTime.Now;
        TemperaturaMinimaConfig = temperaturaMinima;
        TemperaturaMaximaConfig = temperaturaMaxima;
        Modelo = modelo;
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        EstadoHeladera = estadoHeladera;
    }

    public void CambioEstadoSensorTemperatura(float dato)
    {
        if (dato >= TemperaturaMinimaConfig || dato <= TemperaturaMaximaConfig)
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

    public void AgregarSensor(Sensor sensor)
    {
        Sensores.Add(sensor);
    }

    public void EliminarSensor(Sensor sensor)
    {
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