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
    
/* TODO:REVISAR ESTAS FUNCIONES VIEJAS
    public void IngresarViandas(Heladera heladeraOrigen, List<Vianda> viandas)
    {
        if (EstadoHeladera == EstadoHeladera.Activa)
        {
            if (Viandas.Count + viandas.Count <= Capacidad)
            {
                Viandas.AddRange(viandas);
            }
            else
            {
                throw new Exception("No hay espacio suficiente en la heladera");
            }
        }
        else
        {
            throw new Exception("La heladera no está funcionando");
        }
    }

    public List<Vianda> RetirarViandas(int cantidad)
    {
        if (Viandas.Count - cantidad >= 0)
        {
            var viandas = Viandas.GetRange(0, cantidad);
            Viandas.RemoveRange(0, cantidad);
            return viandas;
        }
        else
        {
            throw new Exception("No hay suficientes viandas en la heladera");
        }
    }
*/
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
        _sensores.Add(sensor);
    }
    
    public void EliminarSensor(ISensor sensor){
        _sensores.Remove(sensor);
    }

    public int ObtenerCantidadDeViandas()
    {
        return _viandas.Count;
    }

    public EstadoHeladera ObtenerEstadoHeladera()
    {
        return _estadoHeladera;
    }

    public float ObtenerLatitud()
    {
        return PuntoEstrategico.Latitud;
    }

    public float ObtenerLongitud()
    {
        return PuntoEstrategico.Longitud;
    }

/* TODO:REVISAR ESTAS FUNCIONES VIEJAS
    public bool VerificarTemperatura(float temperatura)
    {
        return temperatura >= TemperaturaMinima && temperatura <= TemperaturaMaxima;
    }

    public void ActualizarRangoTemperatura(float temperaturaMinima, float temperaturaMaxima)
    {
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }

    public EstadoHeladera EstadoHeladera => _estadoHeladera;


    */
}