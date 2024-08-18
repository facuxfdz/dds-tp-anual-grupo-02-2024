namespace AccesoAlimentario.API.Controllers;

public class RegistroTemperaturaProcessor : IRabbitMessageProcessor
{
    public void ProcessMessage(string message)
    {
        // TODO: Implementar lógica de procesamiento de mensaje de temperatura
        Console.WriteLine("Nueva temperatura de heladera recivida");
        Console.WriteLine("Procesando mensaje de temperatura: " + message);
    }
}