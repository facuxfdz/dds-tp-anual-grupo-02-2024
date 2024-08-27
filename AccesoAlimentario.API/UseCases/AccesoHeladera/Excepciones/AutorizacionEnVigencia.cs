namespace AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;

public class AutorizacionEnVigencia : Exception
{
    public AutorizacionEnVigencia(DateTime fechaExpiracion) : base("Ya existe una autorización en vigencia para esa heladera y tarjeta. Fecha de expiración: " + fechaExpiracion)
    {
    }
}