using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class ViandaEstandar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public float Largo { get; private set; } = 0;
    public float Ancho { get; private set; } = 0;
    public float Profundidad { get; private set; } = 0;

    public ViandaEstandar()
    {
    }

    public ViandaEstandar(float largo, float ancho, float profundidad)
    {
        Largo = largo;
        Ancho = ancho;
        Profundidad = profundidad;
    }

    public void Actualizar(float largo, float ancho, float profundidad)
    {
        Largo = largo;
        Ancho = ancho;
        Profundidad = profundidad;
    }
}