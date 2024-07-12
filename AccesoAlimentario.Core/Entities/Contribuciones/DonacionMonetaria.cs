using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionMonetaria : FormaContribucion
{
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorDonacionMonetaria();
    private float _monto;
    private int _frecuenciaDias;

    public DonacionMonetaria(DateTime fechaContribucion, float monto, int frecuenciaDias) 
    : base(fechaContribucion)
    {
        _monto = monto;
        _frecuenciaDias = frecuenciaDias;
    }

}