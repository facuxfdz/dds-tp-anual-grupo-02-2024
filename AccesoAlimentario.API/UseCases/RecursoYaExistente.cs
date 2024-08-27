namespace AccesoAlimentario.API.UseCases;

public class RecursoYaExistente : Exception
{
    public RecursoYaExistente() : base("El recurso que intenta crear ya existe")
    {
    }
}