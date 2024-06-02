using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionMonetaria : FormaContribucion
{
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorDonacionMonetaria();
    private float _monto;

    public DonacionMonetaria(DateTime fechaContribucion, float monto) : base(fechaContribucion)
    {
        _monto = monto;
    }

    public override float CalcularPuntos()
    {
        return AppSettings.Instance.PesoDonadosCoef * _monto;
    }
}