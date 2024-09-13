using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.Repositories;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

public class RegistrarFraudeHeladera : IRegistrarEventoHeladera
{
    private GenericRepository<RegistroFraude> _registroFraudeRepository;
    public RegistrarFraudeHeladera(GenericRepository<RegistroFraude> registroFraudeRepository)
    {
        _registroFraudeRepository = registroFraudeRepository;
    }
    public void RegistrarEvento(string message)
    {
        // Por el momento la logica de parseo esta aca pero podria parsearse en una capa superior
        // Y podria trabajarse con un objeto de dominio en vez de un string
        Console.WriteLine("RegistrarFraudeHeladera: " + message);
    }
}