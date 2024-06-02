using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Scoring;

public class Score
{
    public void Calcular(Colaborador colaborador, FormaContribucion formaDeContribucion)
    {
        colaborador.AgregarPuntos(formaDeContribucion.CalcularPuntos());
    }
}