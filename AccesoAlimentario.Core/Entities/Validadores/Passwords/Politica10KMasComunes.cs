namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class Politica10KMasComunes : IPoliticaValidacion
{
    private String _pathArchivo = "Resources/10k-most-common.txt";
    public bool Valida(string password)
    {
        return true;
    }
}