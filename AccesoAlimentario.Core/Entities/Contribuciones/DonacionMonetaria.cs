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
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorDonacionMonetaria();
    private float _monto;
    private int _frecuenciaDias;

    public DonacionMonetaria()
    {
    }
    public DonacionMonetaria(DateTime fechaContribucion, float monto, int frecuenciaDias) 
    : base(fechaContribucion)
    {
        _monto = monto;
        _frecuenciaDias = frecuenciaDias;
    }

}