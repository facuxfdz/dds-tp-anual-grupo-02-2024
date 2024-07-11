using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;

namespace AccesoAlimentario.Core.Servicios;

public class PersonasServicio {
    public void CrearHumana(string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, string apellido, SexoDocumento sexo)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>(medioContacto);
        PersonaHumana persona = new PersonaHumana(nombre, apellido, documento, direccion, mediosContacto, apellido, sexo);
    }

    public void CrearJuridica(string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, TipoJuridico tipoJuridico, string rubro)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>(medioContacto);
        PersonaJuridica persona = new PersonaJuridica(nombre, documento, direccion, mediosContacto, tipoJuridico, rubro);
    }

    public void Eliminar(Persona persona)
    {
        //TODO
    }

    public void ModificarHumana(PersonaHumana persona, string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, string apellido, SexoDocumento sexo)
    {
        persona.Nombre = nombre;
        persona.Apellido = apellido;
        persona.Documento = documento;
        persona.Direccion = direccion;
        persona.MediosContacto = medioContacto;
        persona.Sexo = sexo;
    }

    public void ModificarJuridica(PersonaJuridica persona, string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, TipoJuridico tipoJuridico, string rubro)
    {
        persona.Nombre = nombre;
        persona.Documento = documento;
        persona.Direccion = direccion;
        persona.MediosContacto = medioContacto;
        persona.TipoJuridico = tipoJuridico;
        persona.Rubro = rubro;
    }

}