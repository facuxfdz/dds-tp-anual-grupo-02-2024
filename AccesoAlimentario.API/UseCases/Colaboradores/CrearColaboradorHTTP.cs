using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Colaboradores;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class CrearColaboradorHTTP(
    IRepository<Colaborador> colaboradorRepository,
    IRepository<Persona> personaRepository
        )
{
    public void CrearColaborador(ColaboradorDTO colaboradorDTO)
    {
        // Chequear si la persona existe
        var persona = personaRepository.Get(
            filter: p => p.Id == colaboradorDTO.Persona.Id
        );
        Persona[] enumerable = persona as Persona[] ?? persona.ToArray();
        if(persona == null || !enumerable.Any())
        {
            throw new PersonaNoExiste();
        }
        
        // Chequear si la persona ya es colaborador
        var colaborador = new Colaborador(
            enumerable.First()
        );
        Console.WriteLine("persona colaborador: " + colaborador.Persona.Id);
        var colaboradorRes = colaboradorRepository.Get(
                filter: c => c.Persona.Id == colaborador.Persona.Id
            );
        // mostrar cantidad de colaboradores
        if(colaboradorRes != null && colaboradorRes.Count() > 0)
        {
            throw new PersonaYaEsColaborador();
        }
        colaboradorRepository.Insert(colaborador);
    }
}