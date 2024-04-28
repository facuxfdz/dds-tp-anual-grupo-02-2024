namespace AccesoAlimentario.Core.Entities.Usuarios;

public interface IPoliticaValidacion
{
    public bool Valida(string password);
}