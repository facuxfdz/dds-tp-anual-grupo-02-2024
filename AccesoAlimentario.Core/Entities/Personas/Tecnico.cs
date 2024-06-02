using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas;

public class Tecnico : Persona
{
    private string _apellido;
    private IMedioContacto _medioContacto;
    private AreaCobertura _areaCobertura;

    public Tecnico(string nombre, string apellido, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, IMedioContacto medioContacto, AreaCobertura areaCobertura) 
        : base(nombre, direccion, documentoIdentidad)
    {
        _apellido = apellido;
        _medioContacto = medioContacto;
        _areaCobertura = areaCobertura;
    }
    
    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        _areaCobertura = areaCobertura;
    }
}