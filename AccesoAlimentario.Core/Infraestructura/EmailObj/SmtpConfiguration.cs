namespace AccesoAlimentario.Core.Infraestructura.EmailObj;

public class SmtpConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}