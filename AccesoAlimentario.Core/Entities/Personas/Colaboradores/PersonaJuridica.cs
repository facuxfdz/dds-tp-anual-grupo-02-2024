using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas.Colaboradores;

public class PersonaJuridica : Colaborador
{
    private TipoJuridico _tipo;
    private string _rubro;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico, string rubro, Direccion? direccion,
        DocumentoIdentidad? documentoIdentidad, Usuario usuario, List<TipoContribucion> tiposDeContribucionesElegidas)
        : base(razonSocial, direccion, documentoIdentidad, usuario, tiposDeContribucionesElegidas)
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
}