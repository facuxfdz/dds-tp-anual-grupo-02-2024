namespace AccesoAlimentario.Core.Entities.Heladeras;

public class Heladera
{
    public int Id { get; set; }
    public PuntoEstrategico PuntoEstrategico { get; private set; }
    public int Capacidad { get; private set; }
    public List<Vianda> Viandas { get; private set; }
    public EstadoHeladera EstadoHeladera { get; private set; }
    public DateTime FechaInstalacion { get; private set; }
    public float TemperaturaMinima { get; private set; }
    public float TemperaturaMaxima { get; private set; }
    
    public Heladera()
    {
    }
    public Heladera(int id, PuntoEstrategico puntoEstrategico, int capacidad, List<Vianda> viandas,
        EstadoHeladera estadoHeladera, DateTime fechaInstalacion, float temperaturaMinima, float temperaturaMaxima)
    {
        Id = id;
        PuntoEstrategico = puntoEstrategico;
        Capacidad = capacidad;
        Viandas = viandas;
        EstadoHeladera = estadoHeladera;
        FechaInstalacion = fechaInstalacion;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }

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

    public void ActualizarEstado(EstadoHeladera estadoHeladera)
    {
        EstadoHeladera = estadoHeladera;
    }

    public bool VerificarTemperatura(float temperatura)
    {
        return temperatura >= TemperaturaMinima && temperatura <= TemperaturaMaxima;
    }

    public void ActualizarRangoTemperatura(float temperaturaMinima, float temperaturaMaxima)
    {
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }
}