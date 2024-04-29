namespace AccesoAlimentario.Core.Entities.Usuarios;

public class Usuario
{
    private string _nombre;
    private string _password;
    private bool _administrador;

    public Usuario(string userName, string password, bool administrador)
    {
        _nombre = userName;
        _password = password;
        _administrador = _administrador;
    }
    
}