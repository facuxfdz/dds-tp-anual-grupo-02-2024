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
    public TipoJuridico Tipo { get; set; }
    public string Rubro { get; set; }

    public PersonaJuridica(int id, string nombre, DocumentoIdentidad documentoIdentidad, Direccion direccion, List<MedioContacto> mediosDeContacto, TipoJuridico tipo, string rubro)
        : base(id, nombre, documentoIdentidad, direccion, mediosDeContacto)
    {
        Tipo = tipo;
        Rubro = rubro;
    }
}