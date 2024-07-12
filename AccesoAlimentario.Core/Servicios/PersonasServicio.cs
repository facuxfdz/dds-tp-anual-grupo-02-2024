using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas.PersonaJuridica;

namespace AccesoAlimentario.Core.Servicios;

public class PersonasServicio {
    public void CrearHumana(int id, string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, string apellido, SexoDocumento sexo)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>();
        mediosContacto.Add(medioContacto);
        PersonaHumana persona = new PersonaHumana(id, nombre, apellido, documento, direccion, mediosContacto, sexo);
    }

    public void CrearJuridica(int id, string nombre, Direccion direccion, DocumentoIdentidad documento, MedioContacto medioContacto, TipoJuridico tipoJuridico, string rubro)
    {
        List<MedioContacto> mediosContacto = new List<MedioContacto>();
        mediosContacto.Add(medioContacto);
        PersonaJuridica persona = new PersonaJuridica(id, nombre, documento, direccion, mediosContacto, tipoJuridico, rubro);
    }

    public void Eliminar(Persona persona)
    {
        //TODO
    }

    public void ModificarHumana(PersonaHumana persona, string? nombre, Direccion? direccion, DocumentoIdentidad? documento, MedioContacto? medioContacto, string? apellido, SexoDocumento? sexo)
    {
        if (nombre != null)
            persona.Nombre = nombre;

        if (apellido != null)
            persona.Apellido = apellido;

        if (documento != null)
            persona.DocumentoIdentidad = documento;

        if (direccion != null)
            persona.Direccion = direccion;

        if (medioContacto != null)
        {
            // persona.MediosDeContacto.Clear();
            // persona.MediosDeContacto.Add(medioContacto);
        }

        if (sexo.HasValue)
            persona.Sexo = sexo.Value;
    }

    public void ModificarJuridica(PersonaJuridica persona, string? nombre, Direccion? direccion, DocumentoIdentidad? documento, MedioContacto? medioContacto, TipoJuridico? tipoJuridico, string? rubro)
    {
        if (nombre != null)
            persona.Nombre = nombre;

        if (documento != null)
            persona.DocumentoIdentidad = documento;

        if (direccion != null)
            persona.Direccion = direccion;

        if (medioContacto != null)
        {
            // persona.MediosDeContacto.Clear();
            // persona.MediosDeContacto.Add(medioContacto);
        }

        if (tipoJuridico != null)
            persona.Tipo = tipoJuridico.Value;

        if (rubro != null)
            persona.Rubro = rubro;
    }

}