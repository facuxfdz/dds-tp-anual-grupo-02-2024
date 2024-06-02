using AccesoAlimentario.Core.Settings;

namespace AccesoAlimentario.Core.Validadores.Usuarios;

public class Politica10KMasComunes : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        var passwords = AppSettings.Instance.Contrasenias;
        return !passwords.Contains(password);
    }
}