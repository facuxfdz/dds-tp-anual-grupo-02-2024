namespace AccesoAlimentario.API.UseCases.Incidentes.Excepciones;

public class TecnicoNoExiste : Exception
{
    public TecnicoNoExiste() : base("El técnico no existe.")
    {
    }
}