using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera : IObserverSensorMovimiento, IObserverSensorTemperatura, ISubjectHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public PuntoEstrategico PuntoEstrategico { get; set; } = null!;
    public List<Vianda> Viandas { get; set; } = [];
    public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
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
        Estado = EstadoHeladera.Activa;
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
    
    public List<Vianda> RetirarViandas(int cantidad)
    {
        var viandasDisponibles = Viandas.Where(vianda => vianda.Estado == EstadoVianda.Disponible).ToList();
        if (cantidad > viandasDisponibles.Count)
        {
            throw new Exception("No hay suficientes viandas");
        }
        var viandasARetirar = viandasDisponibles.GetRange(0, cantidad);
        
        foreach (var vianda in viandasARetirar)
        {
            Viandas.Remove(vianda);
        }
        
        Notificar(CambioHeladeraTipo.CambioViandas);
        
        return viandasARetirar;
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        Estado = estadoHeladera;
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
        return Estado;
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
    
    public Vianda ConsumirVianda()
    {
        var vianda = Viandas.FirstOrDefault(vianda => vianda.Estado == EstadoVianda.Disponible);
        if (vianda == null)
        {
            throw new Exception("No hay viandas disponibles");
        }
        vianda.Estado = EstadoVianda.Consumida;
        Notificar(CambioHeladeraTipo.CambioViandas);
        return vianda;
    }
    
    public void CambiarTemperaturaMinima(float temperatura)
    {
        if (!Modelo.TemperaturaConfiguracionValida(temperatura, TemperaturaMaximaConfig))
        {
            throw new Exception("La temperatura mínima no es válida"); //TODO: No se si esto se hace aca o no
        }

        TemperaturaMinimaConfig = temperatura;
    }

    public void CambiarTemperaturaMaxima(float temperatura)
    {
        if (!Modelo.TemperaturaConfiguracionValida(TemperaturaMinimaConfig, temperatura))
        {
            throw new Exception("La temperatura máxima no es válida"); //TODO: No se si esto se hace aca o no
        }

        TemperaturaMaximaConfig = temperatura;
    }
}