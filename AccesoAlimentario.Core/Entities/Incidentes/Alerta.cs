namespace AccesoAlimentario.Core.Entities.Incidentes;

public class Alerta : Incidente
{
    private TipoAlerta _tipo;
    
    public Alerta(TipoAlerta tipo) : base()
    {
        _tipo = tipo;
    }
}