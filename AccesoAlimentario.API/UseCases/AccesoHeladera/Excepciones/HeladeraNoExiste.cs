namespace AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;

public class HeladeraNoExiste : Exception
{
    public HeladeraNoExiste() : base("La heladera no existe")
    {
    }
}