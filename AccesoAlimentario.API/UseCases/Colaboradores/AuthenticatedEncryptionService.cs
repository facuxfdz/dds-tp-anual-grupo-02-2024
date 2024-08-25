using System.Security.Cryptography;
using System.Text;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class AuthenticatedEncryptionService : IEncryptionService
{
    private readonly byte[] encryptionKey;

    public AuthenticatedEncryptionService(byte[] encryptionKey)
    {
        this.encryptionKey = encryptionKey;
    }
    
    public string Encrypt(string plaintext)
    {
        using var aes = new AesCcm(encryptionKey);
        
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        var ciphertextBytes = new byte[plaintextBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];
        
        aes.Encrypt(nonce, plaintextBytes, ciphertextBytes, tag);
        return new AesGcmCiphertext(nonce, tag, ciphertextBytes).ToString();
    }

    public string Decrypt(string ciphertext)
    {
        var gcmCiphertext = AesGcmCiphertext.FromBase64String(ciphertext);
        
        using var aes = new AesCcm(encryptionKey);
        
        var plaintextBytes = new byte[gcmCiphertext.CiphertextBytes.Length];
        
        aes.Decrypt(gcmCiphertext.Nonce, gcmCiphertext.CiphertextBytes, gcmCiphertext.Tag, plaintextBytes);
        
        return Encoding.UTF8.GetString(plaintextBytes);
    }
}