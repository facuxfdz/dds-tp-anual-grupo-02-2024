namespace AccesoAlimentario.API.UseCases.Colaboraciones.Excepciones;

public class ContribucionInvalida : Exception
{
    public ContribucionInvalida() : base("La contribución es inválida.")
    {
    }
}