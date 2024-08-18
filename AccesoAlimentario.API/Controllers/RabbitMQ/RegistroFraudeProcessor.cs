namespace AccesoAlimentario.API.Controllers;

public class RegistroFraudeProcessor : IRabbitMessageProcessor
{
    public void ProcessMessage(string message)
    {
        // TODO: Implementar lógica de procesamiento de mensaje de fraude
        Console.WriteLine("Nuevo fraude recivido");
        Console.WriteLine("Procesando mensaje de fraude: " + message);
    }
}