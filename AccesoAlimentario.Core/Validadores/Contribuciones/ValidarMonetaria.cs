using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public class ValidarMonetaria : IValidadorContribuciones
{
    private List<TipoColaborador> _colaboradoresValidos;

    public ValidarMonetaria(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }

    public void Validar(FormaContribucion formaContribucion)
    {
        throw new NotImplementedException();
    }
}