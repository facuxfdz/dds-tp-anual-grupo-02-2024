using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionPeriodica : FormaContribucion
{
    private float _monto;
    private int _frecuencia;
    private bool _activo;


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
}