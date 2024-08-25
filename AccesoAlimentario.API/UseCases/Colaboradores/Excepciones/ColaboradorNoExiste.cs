namespace AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;

public class ColaboradorNoExiste : Exception
{
    public ColaboradorNoExiste() : base("Colaborador no existe")
    {
    }
}