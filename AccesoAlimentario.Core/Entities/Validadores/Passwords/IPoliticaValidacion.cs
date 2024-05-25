namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public interface IPoliticaValidacion
{
    public bool Validar(string password);
}