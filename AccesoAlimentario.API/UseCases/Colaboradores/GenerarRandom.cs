using RandomString4Net;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class GenerarRandom(IEncryptionService encryptionService) : IGeneradorCodigoTarjeta
{
    private const int MAX_LENGTH = 11;
    
    public string GenerarCodigo()
    {
        var codigo = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, MAX_LENGTH);
        return encryptionService.Encrypt(codigo);
    }
}