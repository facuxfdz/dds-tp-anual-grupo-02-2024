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
}