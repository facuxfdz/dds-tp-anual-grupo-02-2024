namespace AccesoAlimentario.Core.Entities.Incidentes;

public class Alerta : Incidente
{
    public TipoAlerta Tipo { get; private set; } = TipoAlerta.Conexion;

    public Alerta()
    {
    }

    public Alerta(TipoAlerta tipo) : base()
    {
        Tipo = tipo;
    }
}