namespace AccesoAlimentario.Core.Validadores.Usuarios;

public interface IPoliticaValidacion
{
    public bool Validar(string password);
}