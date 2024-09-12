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
        Console.WriteLine("RegistrarFraudeHeladera: " + message);
    }
}