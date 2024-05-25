using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

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