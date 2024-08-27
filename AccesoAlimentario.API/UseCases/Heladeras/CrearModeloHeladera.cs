using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

namespace AccesoAlimentario.API.UseCases.Heladeras;

public class CrearModeloHeladera(IRepository<ModeloHeladera> repository)
{
    private bool dtoValido(ModeloHeladeraDTO modelo)
    {
        return modelo.Capacidad != null && modelo.TemperaturaMinima != null && modelo.TemperaturaMaxima != null;
    }
    public void Crear(ModeloHeladeraDTO modelo)
    {
        if(!dtoValido(modelo))
        {
            throw new RequestInvalido("Request invalido para crear modelo de heladera");
        }
        var modeloHeladera = new ModeloHeladera(
            capacidad: (float)modelo.Capacidad!,
            temperaturaMinima: (float)modelo.TemperaturaMinima!,
            temperaturaMaxima: (float)modelo.TemperaturaMaxima!
        );
        repository.Insert(modeloHeladera);
    }
}