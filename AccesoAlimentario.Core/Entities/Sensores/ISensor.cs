namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISensor
{//TODO
  
    public void Registrar(DateTime fecha, int valor); //TODO este valor deberia poder ser bool o float
}