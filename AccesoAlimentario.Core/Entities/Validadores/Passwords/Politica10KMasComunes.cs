namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

using System.IO;

public class Politica10KMasComunes : IPoliticaValidacion
{
    private string _pathArchivo = "Resources/10mil-mas-comunes.txt";
    
    public bool Valida(string password)
    {
        return !File.ReadAllText(_pathArchivo).Contains(password);
    }
}