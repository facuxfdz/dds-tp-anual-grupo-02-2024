using AccesoAlimentario.Core.Settings;

namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class Politica10KMasComunes : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        var appSettings = AppSettings.Instance;
        return !appSettings.Contrasenias.Contains(password);
    }
}