using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    // public int Id { get; set; } TODO esto esta por algo?=  
    public PuntoEstrategico PuntoEstrategico;
    private List<Vianda> _viandas;
    private EstadoHeladera _estadoHeladera;
    private DateTime _fechaInstalacion;
    public float TemperaturaMinimaConfig { get; set; }
    public float TemperaturaMaximaConfig { get; set; }
    private List<ISensor> _sensores;
    private List<Incidente> _incidente;
    private ModeloHeladera _modelo; 

    public Heladera(PuntoEstrategico puntoEstrategico,float temperaturaMinima, float temperaturaMaxima, ModeloHeladera modelo)
    {
        PuntoEstrategico = puntoEstrategico;
        _viandas = new List<Vianda>();
        _estadoHeladera = EstadoHeladera.Activa;
        _fechaInstalacion = DateTime.Now;
        TemperaturaMinimaConfig = temperaturaMinima;
        TemperaturaMaximaConfig = temperaturaMaxima;
        _sensores = new List<ISensor>();
        _incidente = new List<Incidente>();
        _modelo = modelo;
    }
    
    public void Nombre(string nombre)
    {
        PuntoEstrategico.Nombre = nombre;
    }
    
/* TODO:REVISAR ESTAS FUNCIONES VIEJAS
    public void IngresarViandas(Heladera heladeraOrigen, List<Vianda> viandas)
    {
        if (_estadoHeladera == EstadoHeladera.Activa)
        {
            if (_viandas.Count + viandas.Count <= _capacidad)
            {
                _viandas.AddRange(viandas);
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
        if (_viandas.Count - cantidad >= 0)
        {
            var viandas = _viandas.GetRange(0, cantidad);
            _viandas.RemoveRange(0, cantidad);
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
        _estadoHeladera = estadoHeladera;
    }

    public void CambioEstadoSensorTemperatura(float dato){
        if(dato >= TemperaturaMinimaConfig || dato <= TemperaturaMaximaConfig){ //TODO cambiarlas a las del fabricante
            //TODO: aca se cambiaria el estado del sensor, pero hay una lista
        }
        else{
            throw new Exception("Rango de temperatura invalido");

        }
    }


    public void CambioEstadoSensorMovimiento(bool dato){
         //TODO: aca se cambiaria el estado del sensor, pero hay una lista
    }

    public void AgregarSensor(ISensor sensor){
        _sensores.Add(sensor);
    }
    
    public void EliminarSensor(ISensor sensor){
        _sensores.Remove(sensor);
    }

    public int ObtenerCantidadDeViandas(){
        return _viandas.Count;
    }

    public EstadoHeladera ObtenerEstadoHeladera(){
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
        return temperatura >= _temperaturaMinima && temperatura <= _temperaturaMaxima;
    }

    public void ActualizarRangoTemperatura(float temperaturaMinima, float temperaturaMaxima)
    {
        _temperaturaMinima = temperaturaMinima;
        _temperaturaMaxima = temperaturaMaxima;
    }

    public EstadoHeladera EstadoHeladera => _estadoHeladera;


    */
}