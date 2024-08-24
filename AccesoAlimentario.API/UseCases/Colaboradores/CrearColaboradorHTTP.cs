using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class CrearColaboradorHTTP(
    GenericRepository<Colaborador> colaboradorRepository,
    GenericRepository<Persona> personaRepository
        )
{
    public void CrearColaborador(ColaboradorDTO colaboradorDTO)
    {
        
        // Chequear si la persona existe
        var persona = personaRepository.Get(
            filter: p => p.Id == colaboradorDTO.Persona.Id
        ) as Persona[];
        if(persona == null)
        {
            throw new PersonaNoExiste();
        }
        if(persona.Length == 0)
        {
            throw new PersonaNoExiste();
        }
        
        // Chequear si la persona ya es colaborador
        var colaborador = new Colaborador(
            persona[0]
        );
        var colaboradorRes = colaboradorRepository.Get(
                filter: c => c.Persona.Id == colaborador.Persona.Id
            ) as Colaborador[];
        if(colaboradorRes != null && colaboradorRes.Length > 0)
        {
            throw new PersonaYaEsColaborador();
        }
        colaboradorRepository.Insert(colaborador);
    }
}