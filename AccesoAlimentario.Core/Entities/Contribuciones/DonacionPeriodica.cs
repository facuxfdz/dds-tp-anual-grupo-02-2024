namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionPeriodica
{
    private float _monto;
    private int _frecuencia;
    private bool _activo;

    public DonacionPeriodica(float monto, int frecuencia, bool activo)
    {
        _monto = monto;
        _frecuencia = frecuencia;
        _activo = activo;
    }

    public void Colaborar()
    {
        
    }
    
}