using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Validaciones.Passwords;

public class PoliticaLongitud : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        return password.Length >= 8;
    }
}