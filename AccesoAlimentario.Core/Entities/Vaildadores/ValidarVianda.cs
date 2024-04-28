using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Vaildadores;

public class ValidarVianda
{
    private List<TipoColaborador> _colaboradoresValidos;
    public ValidarVianda(List<TipoColaborador> colaboradoresValidos)
    {
        _colaboradoresValidos = colaboradoresValidos;
    }
    public void Validar(FormaContribucion formaContribucion)
    {
        
    }
    
}