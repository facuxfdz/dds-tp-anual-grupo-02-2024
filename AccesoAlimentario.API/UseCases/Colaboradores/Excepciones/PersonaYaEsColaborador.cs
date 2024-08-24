namespace AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;

public class PersonaYaEsColaborador : Exception
{
    public PersonaYaEsColaborador() : base("La persona ya es colaborador")
    {
    }
}