using AccesoAlimentario.Core.Settings;

namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

using System.IO;

public class Politica10KMasComunes : IPoliticaValidacion
{
       
    public bool Valida(string password)
    {
        var appSettings = AppSettings.Instance;
        return !appSettings.Contrasenias.Contains(password);
    }
}