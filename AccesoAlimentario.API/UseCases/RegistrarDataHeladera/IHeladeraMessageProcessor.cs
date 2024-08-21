namespace AccesoAlimentario.API.Controllers;

public interface IHeladeraMessageProcessor
{
    public void ProcessMessage(string message);
}