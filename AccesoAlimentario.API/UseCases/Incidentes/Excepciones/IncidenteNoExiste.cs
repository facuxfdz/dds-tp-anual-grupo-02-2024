namespace AccesoAlimentario.API.UseCases.Incidentes.Excepciones;

public class IncidenteNoExiste : Exception
{
    public IncidenteNoExiste() : base("El incidente no existe")
    {
    }
}