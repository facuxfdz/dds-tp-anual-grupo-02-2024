using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Personas.Tecnicos;

public class Tecnico : Rol
{
    private AreaCobertura _areaCobertura;

    public Tecnico(Persona persona, AreaCobertura areaCobertura)
        : base(persona)
    {
        _areaCobertura = areaCobertura;
    }

    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        _areaCobertura = areaCobertura;
    }

    public bool ObtenerCercania(Heladera heladera)
    {
        var longitud = heladera.ObtenerLongitud();
        var latitud = heladera.ObtenerLatitud();
        return _areaCobertura.EsCercano(longitud, latitud);
    }
}