namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class PoliticaLongitud : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        return password.Length >= 8;
    }
}