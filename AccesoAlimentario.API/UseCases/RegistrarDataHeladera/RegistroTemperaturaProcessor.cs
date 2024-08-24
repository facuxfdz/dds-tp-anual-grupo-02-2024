// using System.Text.Json;
// using AccesoAlimentario.Core.Entities.RegistrosSensores;
//
// namespace AccesoAlimentario.API.Controllers;
//
// public class RegistroTemperaturaProcessor : IHeladeraMessageProcessor
// {
//     private RegistrarTemperaturaHeladera _registrarTemperaturaHeladera;
//     public RegistroTemperaturaProcessor(RegistrarTemperaturaHeladera registrarTemperaturaHeladera)
//     {
//         _registrarTemperaturaHeladera = registrarTemperaturaHeladera;
//     }
//     public void ProcessMessage(string message)
//     {
//         Console.WriteLine("Nueva temperatura de heladera recivida");
//         Console.WriteLine("Procesando mensaje de temperatura: " + message);
//         RegistroTemperatura? registroTemperatura = JsonSerializer.Deserialize<RegistroTemperatura>(message);
//         if(registroTemperatura == null)
//         {
//             Console.WriteLine("Error al deserializar el mensaje de temperatura");
//             return;
//         }
//         _registrarTemperaturaHeladera.RegistrarTemperatura(registroTemperatura);
//     }
// }