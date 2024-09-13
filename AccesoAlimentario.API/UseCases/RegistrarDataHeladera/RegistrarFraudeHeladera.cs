using System.Text.Json;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.RegistrarDataHeladera.DTO;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

public class RegistrarFraudeHeladera : IRegistrarEventoHeladera
{
    private IRepository<RegistroFraude> _registroFraudeRepository;
    private IRepository<Heladera> _heladeraRepository;
    public RegistrarFraudeHeladera(IQueueFraudeConsumerUOW unitofWork)
    {
        _registroFraudeRepository = unitofWork.RegistroFraudeRepository;
        _heladeraRepository = unitofWork.HeladeraRepository;
    }
    public void RegistrarEvento(List<string> messages)
    {
        // Por el momento la logica de parseo esta aca pero podria parsearse en una capa superior
        // Y podria trabajarse con un objeto de dominio en vez de un string
        Console.WriteLine("Nuevos registros de fraude recibidos");
        foreach (var message in messages)
        {
            // Parseamos el mensaje
            RegistroFraudeDto? registroFraudeDto = JsonSerializer.Deserialize<RegistroFraudeDto>(message);
            if (registroFraudeDto == null)
            {
                Console.WriteLine("Error al deserializar el mensaje de fraude");
                return;
            }

            // Convertimos el DTO a la entidad de dominio
            var heladera = _heladeraRepository.GetById(registroFraudeDto.Heladera);
            if (heladera == null)
            {
                Console.WriteLine($"No se encontró la heladera con ID {registroFraudeDto.Heladera}");
                continue;
            }
            
            var registroFraude = new RegistroFraude(heladera, registroFraudeDto.FechaLectura);
        
            // Insertamos el registro en el repositorio
            _registroFraudeRepository.Insert(registroFraude);
        }
    }
}