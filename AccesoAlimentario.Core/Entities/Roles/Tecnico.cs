using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Tecnico : Rol
{
    public AreaCobertura AreaCobertura { get; set; } = null!;
    
    public Tecnico()
    {
    }
    
    public Tecnico(Persona persona, AreaCobertura areaCobertura) : base(persona)
    {
        AreaCobertura = areaCobertura;
    }

    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        AreaCobertura.ActualizarArea(
            areaCobertura.Latitud,
            areaCobertura.Longitud,
            areaCobertura.Radio
        );
    }

    public bool ObtenerCercania(Heladera heladera)
    {
        var longitud = heladera.ObtenerLongitud();
        var latitud = heladera.ObtenerLatitud();
        return AreaCobertura.EsCercano(longitud, latitud);
    }
}