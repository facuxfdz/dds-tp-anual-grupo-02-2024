// using AccesoAlimentario.Core.Entities.RegistrosSensores;
// using System.Text.Json;
//
// namespace AccesoAlimentario.API.Controllers;
//
// public class RegistroFraudeProcessor : IHeladeraMessageProcessor
// {
//     private RegistrarFraudeHeladera _registrarFraudeHeladera;
//     public RegistroFraudeProcessor(RegistrarFraudeHeladera registrarFraudeHeladera)
//     {
//         _registrarFraudeHeladera = registrarFraudeHeladera;
//     }
//     public void ProcessMessage(string message)
//     {
//         // TODO: Implementar lógica de procesamiento de mensaje de fraude
//         Console.WriteLine("Nuevo fraude recivido");
//         Console.WriteLine("Procesando mensaje de fraude: " + message);
//         RegistroFraude? registroFraude = JsonSerializer.Deserialize<RegistroFraude>(message);
//         if(registroFraude == null)
//         {
//             Console.WriteLine("Error al deserializar el mensaje de fraude");
//             return;
//         }
//         _registrarFraudeHeladera.RegistrarFraude(registroFraude);
//     }
// }