namespace AccesoAlimentario.Core.Validadores.Usuarios;

public class PoliticaLongitud : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        return password.Length >= 8;
    }
}