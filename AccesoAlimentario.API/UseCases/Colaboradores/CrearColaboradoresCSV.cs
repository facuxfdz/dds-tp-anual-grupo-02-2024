using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Infrastructure.Repositories;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class CrearColaboradoresCSV(
    GenericRepository<Colaborador> colaboradorRepository,
    GenericRepository<Persona> personaRepository
        )
{
    public void CrearColaborador(ColaboradorDTO colaboradorDTO)
    {
        throw new NotImplementedException();
    }
}