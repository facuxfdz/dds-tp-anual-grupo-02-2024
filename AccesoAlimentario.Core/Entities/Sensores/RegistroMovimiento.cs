namespace AccesoAlimentario.Core.Entities.Sensores;

public class RegistroMovimiento
{
    private DateTime _date;
    private bool _movimiento;
    
    public RegistroMovimiento(DateTime date, bool movimiento)
    {
        _date = date;
        _movimiento = movimiento;
    }
}