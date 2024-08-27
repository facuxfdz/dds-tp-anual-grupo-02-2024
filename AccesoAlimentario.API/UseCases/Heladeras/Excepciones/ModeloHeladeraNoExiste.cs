namespace AccesoAlimentario.API.UseCases.Heladeras.Excepciones;

public class ModeloHeladeraNoExiste : Exception
{
    public ModeloHeladeraNoExiste() : base("El modelo de heladera no existe")
    {
    }
}