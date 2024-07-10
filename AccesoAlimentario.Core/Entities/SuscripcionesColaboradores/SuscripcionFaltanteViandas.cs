using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionFaltanteViandas : Suscripcion
{
    private int _minimo;
    public SuscripcionFaltanteViandas(int minimo, Heladera heladera) : base(heladera)
    {   
        _minimo = minimo;
    }
}