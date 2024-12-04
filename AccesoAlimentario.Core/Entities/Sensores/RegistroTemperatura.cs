namespace AccesoAlimentario.Core.Entities.Sensores;

public class RegistroTemperatura
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public float Temperatura { get; set; } = 0;

    public RegistroTemperatura()
    {
    }

    public RegistroTemperatura(DateTime date, float temperatura)
    {
        Date = date;
        Temperatura = temperatura;
    }
}