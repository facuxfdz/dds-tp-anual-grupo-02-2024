using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Validaciones.Contribuciones;

public class ValidarVianda : IValidadorContribuciones
{
    private List<TipoColaborador> _colaboradoresValidos;

    public ValidarVianda(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }

    public void Validar(FormaContribucion formaContribucion)
    {
        throw new NotImplementedException();
    }
}