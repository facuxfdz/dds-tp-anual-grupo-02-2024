using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionMonetaria : FormaContribucion
{
    public float Monto { get; set; } = 0;
    public int FrecuenciaDias { get; set; } = 0;
    
    public DonacionMonetaria()
    {
    }

    public DonacionMonetaria(DateTime fechaContribucion, float monto, int frecuenciaDias)
        : base(fechaContribucion)
    {
        Monto = monto;
        FrecuenciaDias = frecuenciaDias;
    }

    public override float CalcularPuntos()
    {
        return AppSettings.Instance.PesoDonadosCoef * Monto;
    }
}