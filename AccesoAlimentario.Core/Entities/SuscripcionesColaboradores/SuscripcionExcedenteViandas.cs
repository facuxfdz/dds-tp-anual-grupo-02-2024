using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionExcedenteViandas : Suscripcion
{
    public int Maximo { get; private set; } = 0;

    public SuscripcionExcedenteViandas()
    {
    }

    public SuscripcionExcedenteViandas(int maximo, Heladera heladera) : base(heladera)
    {
        Maximo = maximo;
    }

    public void NotificarColaborador()
    {
        //TODO
    }
}