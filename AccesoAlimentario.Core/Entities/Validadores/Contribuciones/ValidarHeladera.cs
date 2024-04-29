using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

public class ValidarHeladera : IValidadorContribuciones
{
    private List<TipoColaborador> _colaboradoresValidos;
    public ValidarHeladera(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }
    public void Validar(FormaContribucion formaContribucion)
    {
        
    }
    
}