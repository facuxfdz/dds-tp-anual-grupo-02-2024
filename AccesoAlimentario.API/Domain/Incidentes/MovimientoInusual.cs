using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class MovimientoInusual : Alerta
{
    public MovimientoInusual()
    {
    }
    public MovimientoInusual(DateTime fecha, Heladera heladera) : base(fecha, heladera)
    {
    }
}