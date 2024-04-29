namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class PoliticaLongitud : IPoliticaValidacion
{
    public bool Valida(string password)
    {
        return password.Length >= 8;
    }
}