using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;
using AccesoAlimentario.Core.Resources;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionPeriodica : FormaContribucion
{
    private float _monto;
    private int _frecuencia;
    private bool _activo;
    private List<DonacionMonetaria> _donacionesRealizadas;
    // private readonly IValidadorContribuciones validadorContribuciones = new ValidarPeriodica();


    public DonacionPeriodica(Colaborador colaborador,
        DateTime fechaContribucion, float monto, int frecuencia, bool activo)
        : base(colaborador, fechaContribucion)
    {
        _monto = monto;
        _frecuencia = frecuencia;
        _activo = activo;
    }

    public override float CalcularPuntos()
    {
        return Config.PesoDonadosCoef * _monto;
    }
}