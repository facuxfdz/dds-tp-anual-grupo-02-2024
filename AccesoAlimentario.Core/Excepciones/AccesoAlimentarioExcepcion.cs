namespace AccesoAlimentario.Core.Excepciones;

public abstract class AccesoAlimentarioExcepcion : Exception
{
    public string Codigo { get; set; } = string.Empty;
}