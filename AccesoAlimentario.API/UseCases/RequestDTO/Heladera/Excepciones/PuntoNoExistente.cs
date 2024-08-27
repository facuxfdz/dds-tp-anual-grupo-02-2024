namespace AccesoAlimentario.API.UseCases.RequestDTO.Heladera.Excepciones;

public class PuntoNoExistente : Exception
{
    public PuntoNoExistente() : base("El punto estratégico no existe")
    {
    }
}