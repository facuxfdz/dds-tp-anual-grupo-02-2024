using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas.PersonaJuridica;

namespace AccesoAlimentario.Core.Servicios;

public class PersonasServicio {
    public void CrearHumana(string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, string apellido, SexoDocumento sexo)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>();
        mediosContacto.Add(medioContacto);
        PersonaHumana persona = new PersonaHumana(nombre, apellido, documento, direccion, mediosContacto, sexo);
    }

    public void CrearJuridica(string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, TipoJuridico tipoJuridico, string rubro)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>();
        mediosContacto.Add(medioContacto);
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
        persona.DocumentoIdentidad = documento;
        persona.Direccion = direccion;
        persona.MediosDeContacto = medioContacto;
        persona.Sexo = sexo;
    }

    public void ModificarJuridica(PersonaJuridica persona, string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, TipoJuridico tipoJuridico, string rubro)
    {
        persona.Nombre = nombre;
        persona.DocumentoIdentidad = documento;
        persona.Direccion = direccion;
        persona.MediosDeContacto = medioContacto;
        persona.Tipo = tipoJuridico;
        persona.Rubro = rubro;
    }

}