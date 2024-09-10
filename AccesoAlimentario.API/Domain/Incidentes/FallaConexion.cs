using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class FallaConexion : Alerta
{
    public DateTime FechaUltimaConexion { get; set; } = DateTime.Now;

    public FallaConexion(DateTime fecha, Heladera heladera, DateTime fechaUltimaConexion) : base(fecha, heladera)
    {
        FechaUltimaConexion = fechaUltimaConexion;
    }
}