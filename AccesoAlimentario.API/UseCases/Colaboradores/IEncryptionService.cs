namespace AccesoAlimentario.API.UseCases.Colaboradores;

public interface IEncryptionService
{
    public string Encrypt(string plaintext);
    public string Decrypt(string cyphertext);
}