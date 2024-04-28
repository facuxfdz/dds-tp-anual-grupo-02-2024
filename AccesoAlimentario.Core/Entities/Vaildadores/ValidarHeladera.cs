using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Vaildadores;

public class ValidarHeladera
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