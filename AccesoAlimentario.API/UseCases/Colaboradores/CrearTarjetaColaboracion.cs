using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class CrearTarjetaColaboracion(
    IRepository<Colaborador> colaboradorRepository,
    IRepository<TarjetaColaboracion> tarjetaColaboracionRepository,
    IGeneradorCodigoTarjeta generadorCodigoTarjeta
        )
{
    public void CrearTarjeta(ColaboradorDTO colaborador)
    {
        var colaboradorBusq = colaboradorRepository.Get(
            filter: c => c.Id == colaborador.Id,
            includeProperties: "Persona"
        );

        var colaboradores = colaboradorBusq as Colaborador[] ?? colaboradorBusq.ToArray();
        if (!colaboradores.Any())
        {
            throw new ColaboradorNoExiste();
        }
        var persona = colaboradores.First().Persona;
        var codigoTarjeta = generadorCodigoTarjeta.GenerarCodigo();
        var tarjeta = new TarjetaColaboracion(
            codigoTarjeta,
            persona
        );
        tarjetaColaboracionRepository.Insert(tarjeta);
        // Agregar la tarjeta creada al colaborador
        colaboradores.First().AgregarTarjeta(tarjeta);
        colaboradorRepository.Update(colaboradores.First());
    }
}