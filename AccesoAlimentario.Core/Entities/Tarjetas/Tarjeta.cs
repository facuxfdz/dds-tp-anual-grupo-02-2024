using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class Tarjeta
{
    private string _codigo;
    private PersonaVulnerable _propietario;
    private Colaborador _responsable;
    private List<TarjetaConsumo> _consumos;
    
    public Tarjeta(string codigo, Colaborador colaborador)
    {
        _codigo = codigo;
        _responsable = colaborador;
        _consumos = new List<TarjetaConsumo>();
    }
    
    public void RegistrarConsumo(DateTime fecha, Heladera heladera)
    {
        _consumos.Add(new TarjetaConsumo(fecha, heladera));
    }
}