using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Vaildadores;

public class ValidarPeriodica
{
    private List<TipoColaborador> _colaboradoresValidos;
    public ValidarPeriodica(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }
    public void Validar(FormaContribucion formaContribucion)
    {
        
    }
    
}