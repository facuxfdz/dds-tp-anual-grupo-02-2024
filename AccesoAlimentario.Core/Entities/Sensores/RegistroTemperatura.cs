namespace AccesoAlimentario.Core.Entities.Sensores;

public class RegistroTemperatura
{
    private DateTime _date;
    private float _temperatura;
    
    public RegistroTemperatura(DateTime date, float temperatura)
    {
        _date = date;
        _temperatura = temperatura;
    }
}