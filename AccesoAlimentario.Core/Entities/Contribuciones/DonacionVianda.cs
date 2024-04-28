using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda
{
    private Heladera _heladera;
    private Vianda _vianda;

    public DonacionVianda(Heladera heladera, Vianda vianda)
    {
        _heladera = heladera;
        _vianda = vianda;
    }

    public void Colaborar()
    {
        
    }
}