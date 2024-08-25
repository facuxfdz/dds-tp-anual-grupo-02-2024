namespace AccesoAlimentario.API.Infrastructure.Controllers;

public class RequestInvalido : Exception
{
    public RequestInvalido(string message) : base(message)
    {
    }
}