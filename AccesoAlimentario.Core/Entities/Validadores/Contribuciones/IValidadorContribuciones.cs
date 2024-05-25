using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

public interface IValidadorContribuciones
{
    public void Validar(FormaContribucion formaContribucion);
}