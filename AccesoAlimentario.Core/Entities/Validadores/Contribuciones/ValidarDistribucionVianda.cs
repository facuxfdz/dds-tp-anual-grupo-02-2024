using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

public class ValidarDistribucionVianda : IValidadorContribuciones
{
    private List<TipoColaborador> _colaboradoresValidos;


    public ValidarDistribucionVianda(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }
    public void Validar(FormaContribucion formaContribucion)
    {
        
    }
}