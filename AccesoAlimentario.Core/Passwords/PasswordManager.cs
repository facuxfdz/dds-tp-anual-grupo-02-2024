namespace AccesoAlimentario.Core.Passwords;

public static class PasswordManager
{
    public static string CrearPassword()
    {
        const int length = 16;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public static string HashPassword(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hashBytes = System.Security.Cryptography.SHA256.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}