using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Core.Validadores.Passwords;

public class Politica10KMasComunes : IPoliticaValidacion
{
    private string _pathArchivo = "Resources/10mil-mas-comunes.txt";
    
    public bool Valida(string password)
    {
        return !File.ReadAllText(_pathArchivo).Contains(password);
    }

    public bool Validar(string password)
    {
        throw new NotImplementedException();
    }
}