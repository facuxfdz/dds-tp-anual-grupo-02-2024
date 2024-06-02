using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Entities.Scoring;

public class Score
{
    public void Calcular(Colaborador colaborador, FormaContribucion formaDeContribucion)
    {
        colaborador.AgregarPuntos(formaDeContribucion.CalcularPuntos());
    }
}