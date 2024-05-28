using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Scoring;

public class Score
{
    public void Calcular(Colaborador colaborador, FormaContribucion formaDeContribucion)
    {
        colaborador.AgregarPuntos(formaDeContribucion.CalcularPuntos());
    }
}