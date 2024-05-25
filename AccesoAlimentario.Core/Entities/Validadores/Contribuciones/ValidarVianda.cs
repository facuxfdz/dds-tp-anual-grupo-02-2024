using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

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