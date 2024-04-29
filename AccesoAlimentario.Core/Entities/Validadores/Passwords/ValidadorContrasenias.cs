namespace AccesoAlimentario.Core.Entities.Validadores.Passwords;

public class ValidadorContrasenias
{
    private List<IPoliticaValidacion> _validaciones;
    
    public ValidadorContrasenias(List<IPoliticaValidacion> validaciones)
    {
        _validaciones = validaciones;
    }
    
    public bool Validar(string password)
    {
        foreach (var validacion in _validaciones)
        {
            if (!validacion.Valida(password))
            {
                return false;
            }
        }
        return true;
    }
}