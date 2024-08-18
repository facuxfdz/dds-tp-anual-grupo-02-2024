namespace AccesoAlimentario.API.Controllers;

public interface IRabbitMessageProcessor
{
    public void ProcessMessage(string message);
}