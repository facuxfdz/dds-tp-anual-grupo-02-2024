using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    public Colaborador Responsable { get; set; } = null!;
    public new PersonaVulnerable Propietario { get; set; } = null!;

    public bool PuedeConsumir()
    {
        var consumosDeHoy = Accesos
            .Where(acceso =>
                acceso.FechaAcceso.Date == DateTime.Now.Date 
                && acceso.TipoAcceso == TipoAcceso.RetiroVianda);

        var cosumosMaximosPermitidos = 4 + 2 * Propietario.CantidadDeMenores;
        
        return consumosDeHoy.Count() < cosumosMaximosPermitidos;
    }
}