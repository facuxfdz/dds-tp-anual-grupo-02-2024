namespace AccesoAlimentario.API.Domain.Heladeras.Excepciones;

public class ExtraccionInvalidaDeViandas : Exception
{
    // Esta excepcion se lanza cuando se intenta extraer una cantidad invalida de viandas de una heladera
    // Intentar extraer mas viandas de las que hay en la heladera es una extraccion invalida
    public ExtraccionInvalidaDeViandas() : base("Extraccion invalida de viandas")
    {
    } 
}