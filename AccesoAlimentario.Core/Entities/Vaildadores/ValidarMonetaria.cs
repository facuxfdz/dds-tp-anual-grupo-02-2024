using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Vaildadores;

public class ValidarMonetaria
{
    private List<TipoColaborador> _colaboradoresValidos;
    public ValidarMonetaria(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }
    public void Validar(FormaContribucion formaContribucion)
    {
        
    }
    
}