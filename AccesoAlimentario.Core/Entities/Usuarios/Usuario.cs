namespace AccesoAlimentario.Core.Entities.Usuarios;

public class Usuario
{
    private string _userName;
    private string _password;
    private string _email;
    // private DateOffSet _fechaAlta;
    private bool _administrador;

    public Usuario(string userName, string password, string email)
    {
        _userName = userName;
        _password = password;
        _email = email;
    }
}