namespace AccesoAlimentario.API.Domain.Incidentes;

public class FallaConexion : Alerta
{
    public DateTime FechaUltimaConexion { get; set; }
    
    public FallaConexion()
    {
    }
    
    public FallaConexion(DateTime fecha, NivelAlerta nivel, DateTime fechaUltimaConexion) : base(fecha, nivel)
    {
        FechaUltimaConexion = fechaUltimaConexion;
    }
}