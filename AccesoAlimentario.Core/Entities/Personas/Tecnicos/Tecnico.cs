using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas.Tecnicos;

public class Tecnico : Persona
{
    private string _apellido;
    private MedioContacto _medioContacto;
    private AreaCobertura _areaCobertura;

    public Tecnico()
    {
    }
    public Tecnico(int id, string nombre, string apellido, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, MedioContacto medioContacto, AreaCobertura areaCobertura) 
        : base(id, nombre, direccion, documentoIdentidad)
    {
        Id = id;
        _apellido = apellido;
        _medioContacto = medioContacto;
        _areaCobertura = areaCobertura;
    }
    
    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        _areaCobertura = areaCobertura;
    }
}