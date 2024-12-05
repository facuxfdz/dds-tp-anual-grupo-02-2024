namespace AccesoAlimentario.Core.Infraestructura.EmailObj;

public class SmtpConfiguration
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}