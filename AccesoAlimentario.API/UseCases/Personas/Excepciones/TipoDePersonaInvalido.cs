namespace AccesoAlimentario.API.UseCases.Personas.Excepciones;

public class TipoDePersonaInvalido : Exception
{
    public TipoDePersonaInvalido() : base("El tipo de persona es inválido.")
    {
    }
}