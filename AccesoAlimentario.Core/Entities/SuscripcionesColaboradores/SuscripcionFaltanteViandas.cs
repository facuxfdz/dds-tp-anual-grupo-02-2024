using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionFaltanteViandas : Suscripcion
{
    public int Minimo { get; private set; } = 0;
    
    public SuscripcionFaltanteViandas()
    {
    }
    
    public SuscripcionFaltanteViandas(int minimo, Heladera heladera) : base(heladera)
    {   
        Minimo = minimo;
    }

    public void NotificarColaborador()
    {
        //TODO
    }
}