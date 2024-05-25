using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo
{
    private DateTime _fecha;
    private Heladera _heladera;
    
    public TarjetaConsumo(DateTime fecha, Heladera heladera)
    {
        _fecha = fecha;
        _heladera = heladera;
    }
}