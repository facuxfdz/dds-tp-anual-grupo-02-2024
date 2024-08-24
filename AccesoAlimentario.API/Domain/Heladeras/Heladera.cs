using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Heladeras.Excepciones;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

namespace AccesoAlimentario.API.Domain.Heladeras;

public class Heladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public PuntoEstrategico PuntoEstrategico { get; set; } = null!;
    public List<Vianda> Viandas { get; set; } = null!;
    public EstadoHeladera EstadoHeladera { get; set; } = EstadoHeladera.FueraServicio;
    public DateTime FechaInstalacion { get; set; } = DateTime.Now;
    public float TemperaturaActual { get; set; } = 0;
    public float TemperaturaMinimaConfig { get; set; } = 0;
    public float TemperaturaMaximaConfig { get; set; } = 0;
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

    public void IngresarVianda(Vianda vianda)
    {
        Viandas.Add(vianda);
    }
    
    public bool RetirarViandas(int cantidad)
    {
        if (cantidad > Viandas.Count)
        {
            return false;
        }
        Viandas.RemoveRange(0, cantidad);
        return true;
    }

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        EstadoHeladera = estadoHeladera;
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