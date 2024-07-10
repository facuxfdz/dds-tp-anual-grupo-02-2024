using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas.Colaboradores;

public class PersonaJuridica : Colaborador
{
    public TipoJuridico Tipo { get; private set; }
    public string Rubro { get; private set; }

    public PersonaJuridica()
    {
    }

    public PersonaJuridica(int id, string razonSocial, TipoJuridico tipoJuridico, string rubro, Direccion? direccion,
        DocumentoIdentidad? documentoIdentidad, Usuario usuario, List<TipoContribucion> tiposDeContribucionesElegidas)
        : base(id, razonSocial, direccion, documentoIdentidad, usuario, tiposDeContribucionesElegidas)
    {
        Id = id;
        Tipo = tipoJuridico;
        Rubro = rubro;
    }
    
    public void Actualizar(string razonSocial, TipoJuridico tipo, string rubro, Direccion direccion, DocumentoIdentidad docId)
    {
        _nombre = razonSocial;
        Tipo = tipo;
        Rubro = rubro;
        _direccion = direccion;
        _documentoIdentidad = docId;
    }
}