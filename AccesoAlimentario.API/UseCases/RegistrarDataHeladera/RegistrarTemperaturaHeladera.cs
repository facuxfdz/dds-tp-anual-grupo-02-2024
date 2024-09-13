
using System.Text.Json;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.RegistrarDataHeladera.DTO;
using AccesoAlimentario.API.UseCases.Utils;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

public class RegistrarTemperaturaHeladera : IRegistrarEventoHeladera
{
    private IRepository<RegistroTemperatura> _registroTemperaturaRepository;
    private IRepository<Heladera> _heladeraRepository;
    public RegistrarTemperaturaHeladera(
        IQueueTemperaturaConsumerUOW unitOfWork
        )
    {
        _registroTemperaturaRepository = unitOfWork.RegistroTemperaturaRepository;
        _heladeraRepository = unitOfWork.HeladeraRepository;
    }
    public void RegistrarEvento(List<string> messages)
    {
        Console.WriteLine("Nuevos registros de temperatura de heladera recividos");
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new DateTimeConverter() }
        };
        foreach (var message in messages)
        {
            RegistroTemperaturaDto? registroTemperaturaDto = JsonSerializer.Deserialize<RegistroTemperaturaDto>(message);
            if (registroTemperaturaDto == null)
            {
                Console.WriteLine("Error al deserializar el mensaje de temperatura");
                return;
            }

            // Convertimos el DTO a la entidad de dominio
            var heladera = _heladeraRepository.GetById(registroTemperaturaDto.Heladera);
            if (heladera == null)
            {
                Console.WriteLine($"No se encontró la heladera con ID {registroTemperaturaDto.Heladera}");
                continue;
            }
            
            var registroTemperatura = new RegistroTemperatura(heladera, registroTemperaturaDto.FechaLectura, registroTemperaturaDto.Temperatura);
        
            // Insertamos el registro en el repositorio
            _registroTemperaturaRepository.Insert(registroTemperatura);
        }
    }
}