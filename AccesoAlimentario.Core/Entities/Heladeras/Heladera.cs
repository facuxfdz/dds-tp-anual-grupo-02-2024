using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera : IObserverSensorMovimiento, IObserverSensorTemperatura, ISubjectHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public PuntoEstrategico PuntoEstrategico { get; set; } = null!;
    public List<Vianda> Viandas { get; set; } = [];
    public EstadoHeladera EstadoHeladera { get; set; } = EstadoHeladera.FueraServicio;
    public DateTime FechaInstalacion { get; set; } = DateTime.Now;
    public float TemperaturaActual { get; set; } = 0;
    public float TemperaturaMinimaConfig { get; set; } = 0;
    public float TemperaturaMaximaConfig { get; set; } = 0;
    public List<Sensor> Sensores { get; set; } = [];
    public List<Incidente> Incidentes { get; set; } = [];
    public ModeloHeladera Modelo { get; set; } = null!;
    
    private List<IObserverHeladera> Observers { get; set; } = [];

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

    public void IngresarVianda(Vianda vianda)
    {
        Viandas.Add(vianda);
        Notificar(CambioHeladeraTipo.CambioViandas);
    }
    
    public void RetirarViandas(int cantidad)
    {
        if (cantidad > Viandas.Count)
        {
            Notificar(CambioHeladeraTipo.CambioViandas);
            throw new Exception("No hay suficientes viandas");
        }
        Viandas.RemoveRange(0, cantidad);
        Notificar(CambioHeladeraTipo.CambioViandas);
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        EstadoHeladera = estadoHeladera;
    }

    public void CambioSensorTemperatura(float dato, bool error)
    {
        if (error)
        {
            AgregarIncidente(new Alerta(TipoAlerta.Conexion));
        }
        TemperaturaActual = dato;
        if (dato <= TemperaturaMinimaConfig || dato >= TemperaturaMaximaConfig)
        {
            AgregarIncidente(new Alerta(TipoAlerta.Temperatura));
        }
    }

    public void Suscribirse(IObserverHeladera observer)
    {
        Observers.Add(observer);
    }
    
    public void Desuscribirse(IObserverHeladera observer)
    {
        Observers.Remove(observer);
    }
    
    public void Notificar(CambioHeladeraTipo cambio)
    {
        foreach (var observer in Observers)
        {
            observer.CambioHeladera(this, cambio);
        }
    }

    public void CambioSensorMovimiento(bool dato, bool error) 
    {
        if (dato)
        {
            AgregarIncidente(new Alerta(TipoAlerta.Fraude));
        }
        if (error)
        {
            AgregarIncidente(new Alerta(TipoAlerta.Conexion));
        }
    }

    public void AgregarSensor(Sensor sensor)
    {
        Sensores.Add(sensor);
        switch (sensor)
        {
            case ISubjectHeladeraTemperatura observerSensorTemperatura:
                observerSensorTemperatura.Suscribirse(this);
                break;
            case ISubjectHeladeraMovimiento observerSensorMovimiento:
                observerSensorMovimiento.Suscribirse(this);
                break;
        }
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
    
    public void AgregarIncidente(Incidente incidente)
    {
        Incidentes.Add(incidente);
        Notificar(CambioHeladeraTipo.IncidenteProducido);
    }
}