namespace AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;

public class AccesoNoAutorizado : Exception
{
    public AccesoNoAutorizado() : base("El acceso no está autorizado.")
    {
    }
}