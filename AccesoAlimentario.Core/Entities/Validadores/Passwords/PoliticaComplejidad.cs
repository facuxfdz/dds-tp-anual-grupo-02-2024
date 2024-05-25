namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class PoliticaComplejidad : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        return password.All(EsCaracterValido);
    }
    
    public bool EsCaracterValido(char caracter)
    {
        return EsCaracterAscii(caracter) || EsCaracterUnicode(caracter);
    }
    
    public bool EsCaracterAscii(char caracter)
    {
        return (caracter >= 32 && caracter <= 126) || (caracter >= 128 && caracter <= 255);
    }
    
    public bool EsCaracterUnicode(char caracter)
    {
        return (caracter >= 256 && caracter <= 9835);
    }
    
}