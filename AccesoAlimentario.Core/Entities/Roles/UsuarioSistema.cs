using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class UsuarioSistema : Rol
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; } = string.Empty;
    public RegisterType RegisterType { get; set; } = RegisterType.Standard;
    
    public UsuarioSistema()
    {
    }
    
    public UsuarioSistema(Persona persona, string userName, string password, string profilePicture, RegisterType registerType) : base(persona)
    {
        UserName = userName;
        Password = password;
        ProfilePicture = profilePicture;
        RegisterType = registerType;
    }

    public void Actualizar(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}