namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

public interface IRegistrarEventoHeladera
{
    public void RegistrarEvento(List<string> messages);
}