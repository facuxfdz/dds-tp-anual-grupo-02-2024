namespace AccesoAlimentario.API.Domain.Incidentes;

public class MovimientoInusual : Alerta
{
    public MovimientoInusual()
    {
    }
    
    public MovimientoInusual(DateTime fecha, NivelAlerta nivel) : base(fecha, nivel)
    {
    }
}