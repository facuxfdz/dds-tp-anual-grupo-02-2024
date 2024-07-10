using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionExcedenteViandas : Suscripcion
{
    private int _maximo;
    public SuscripcionExcedenteViandas(int maximo, Heladera heladera) : base(heladera)
    {   
        _maximo = maximo;
    }
}