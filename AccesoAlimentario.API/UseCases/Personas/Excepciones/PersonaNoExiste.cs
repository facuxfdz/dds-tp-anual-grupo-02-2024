namespace AccesoAlimentario.API.UseCases.Personas.Excepciones;

public class PersonaNoExiste : Exception
{
    public PersonaNoExiste() : base("La persona no existe")
    {
    }
}