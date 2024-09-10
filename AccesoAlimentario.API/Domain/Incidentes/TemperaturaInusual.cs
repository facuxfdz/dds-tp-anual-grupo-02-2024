using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class TemperaturaInusual : Alerta
{
    public double Temperatura { get; set; } = 0.0;

    public TemperaturaInusual(DateTime fecha, Heladera heladera, double temperatura) : base(fecha, heladera)
    {
        Temperatura = temperatura;
    }
}