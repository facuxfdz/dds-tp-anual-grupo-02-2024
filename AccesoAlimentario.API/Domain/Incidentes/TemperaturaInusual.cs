namespace AccesoAlimentario.API.Domain.Incidentes;

public class TemperaturaInusual : Alerta
{
    public double Temperatura { get; set; }
    
    public TemperaturaInusual()
    {
    }
    
    public TemperaturaInusual(DateTime fecha, NivelAlerta nivel, double temperatura) : base(fecha, nivel)
    {
        Temperatura = temperatura;
    }
}