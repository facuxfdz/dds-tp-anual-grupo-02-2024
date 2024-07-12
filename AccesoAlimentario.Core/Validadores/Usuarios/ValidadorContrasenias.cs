namespace AccesoAlimentario.Core.Validadores.Usuarios;

public class ValidadorContrasenias
{
    private List<IPoliticaValidacion> _validaciones;
    
    public ValidadorContrasenias(List<IPoliticaValidacion> validaciones)
    {
        _validaciones = validaciones;
    }

    public ValidadorContrasenias()
    {
        _validaciones =
        [
            new PoliticaLongitud(),
            new Politica10KMasComunes(),
            new PoliticaComplejidad()
        ];
    }
    
    public bool Validar(string password)
    {
        return _validaciones.All(validacion => validacion.Validar(password));
    }
}