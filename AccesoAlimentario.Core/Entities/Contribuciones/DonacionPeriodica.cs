using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;
using AccesoAlimentario.Core.Resources;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionPeriodica : FormaContribucion
{
    private float _monto;
    private int _frecuencia;
    private bool _activo;
    private List<DonacionMonetaria> _donacionesRealizadas;


    public DonacionPeriodica(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, float monto, int frecuencia, bool activo)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _monto = monto;
        _frecuencia = frecuencia;
        _activo = activo;
    }

    public override void Colaborar()
    {
        throw new NotImplementedException();
    }

    public override float CalcularPuntos()
    {
        return Config.PesoDonadosCoef * _monto;
    }
}