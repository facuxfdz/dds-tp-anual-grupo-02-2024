using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Vaildadores;

public class ValidarDistribucionVianda
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