namespace AccesoAlimentario.Core.Validadores.Usuarios;

public class PoliticaComplejidad : IPoliticaValidacion
{
    public bool Validar(string password)
    {
        return password.All(EsCaracterValido);
    }
    
    private bool EsCaracterValido(char caracter)
    {
        return EsCaracterAscii(caracter) || EsCaracterUnicode(caracter);
    }
    
    private bool EsCaracterAscii(char caracter)
    {
        return (caracter >= 32 && caracter <= 126) || (caracter >= 128 && caracter <= 255);
    }
    
    private bool EsCaracterUnicode(char caracter)
    {
        return (caracter >= 256 && caracter <= 9835);
    }
    
}