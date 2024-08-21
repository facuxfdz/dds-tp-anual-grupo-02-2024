using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.RegistrosSensores;

namespace AccesoAlimentario.API.Controllers;

public class RegistrarFraudeHeladera
{
    private GenericRepository<RegistroFraude> _registroFraudeRepository;
    public RegistrarFraudeHeladera(GenericRepository<RegistroFraude> registroFraudeRepository)
    {
        _registroFraudeRepository = registroFraudeRepository;
    }
    public void RegistrarFraude(RegistroFraude registroFraude)
    {
        _registroFraudeRepository.Insert(registroFraude);
    }
}