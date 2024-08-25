namespace AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;

public class TarjetaInexistente : Exception
{
    public TarjetaInexistente() : base("La tarjeta que intenta utilizar no existe.")
    {
    }
}