using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Personas.PersonaJuridica;

public class PersonaJuridica : Persona
{
    private TipoJuridico _tipo;
    private string _rubro;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico, string rubro, Direccion? direccion,
        DocumentoIdentidad? documentoIdentidad, List<MedioContacto> mediosDeContacto, List<Rol> roles)
        : base(razonSocial, direccion, documentoIdentidad, mediosDeContacto, roles)
    {
        _tipo = tipoJuridico;
        _rubro = rubro;
    }
    
    public void Actualizar(string razonSocial, TipoJuridico tipo, string rubro, Direccion direccion, DocumentoIdentidad docId)
    {
        _nombre = razonSocial;
        _tipo = tipo;
        _rubro = rubro;
        _direccion = direccion;
        _documentoIdentidad = docId;
    }
    
    public override TipoColaborador ObtenerTipoPersona()
    {
        return TipoColaborador.PersonaJuridica;
    }
}