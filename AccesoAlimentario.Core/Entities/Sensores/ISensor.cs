namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISensor
{
    public void Registrar(DateTime fecha, string valor);
}