using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionMonetaria : FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public float Monto { get; private set; }
    private int _frecuenciaDias;

    public DonacionMonetaria()
    {
    }
    public DonacionMonetaria(DateTime fechaContribucion, float monto, int frecuenciaDias) 
    : base(fechaContribucion)
    {
        Monto = monto;
        _frecuenciaDias = frecuenciaDias;
    }

}