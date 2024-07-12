using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Servicios;

public class PersonasServicio(UnitOfWork unitOfWork)
{
    public PersonaHumana CrearHumana(string nombre, Direccion direccion, DocumentoIdentidad documento,
        List<MedioContacto> mediosContacto, string apellido, SexoDocumento sexo)
    {
        PersonaHumana persona = new(nombre, apellido, mediosContacto, direccion, documento, sexo);
        unitOfWork.PersonaHumanaRepository.Insert(persona);
        return persona;
    }

    public PersonaJuridica CrearJuridica(string nombre, Direccion direccion, DocumentoIdentidad documento,
        List<MedioContacto> mediosContacto, TipoJuridico tipoJuridico, string rubro)
    {
        PersonaJuridica persona = new(nombre, tipoJuridico, rubro, mediosContacto, direccion, documento);
        unitOfWork.PersonaJuridicaRepository.Insert(persona);
        return persona;
    }

    public ICollection<Persona> Obtener()
    {
        var personasHumanas = unitOfWork.PersonaHumanaRepository.Get();
        var personasJuridicas = unitOfWork.PersonaJuridicaRepository.Get();
        return personasHumanas.Concat<Persona>(personasJuridicas).ToList();
    }

    public void Eliminar(int id)
    {
        var pH = unitOfWork.PersonaHumanaRepository.GetById(id);
        if (pH != null)
            unitOfWork.PersonaHumanaRepository.Delete(pH);
        else
        {
            var pJ = unitOfWork.PersonaJuridicaRepository.GetById(id);
            if (pJ != null)
                unitOfWork.PersonaJuridicaRepository.Delete(pJ);
        }
    }

    public void ModificarHumana(PersonaHumana persona, string? nombre, Direccion? direccion,
        DocumentoIdentidad? documento, MedioContacto? medioContacto, string? apellido, SexoDocumento? sexo)
    {
        /*if (nombre != null)
            persona.Nombre = nombre;

        if (apellido != null)
            persona.Apellido = apellido;

        if (documento != null)
            persona.DocumentoIdentidad = documento;

        if (direccion != null)
            persona.Direccion = direccion;

        if (medioContacto != null)
        {
            persona.MediosDeContacto.Clear();
            persona.MediosDeContacto.Add(medioContacto);
        }

        if (sexo.HasValue)
            persona.Sexo = sexo.Value;*/
    }

    public void ModificarJuridica(PersonaJuridica persona, string? nombre, Direccion? direccion,
        DocumentoIdentidad? documento, MedioContacto? medioContacto, TipoJuridico? tipoJuridico, string? rubro)
    {
        /*if (nombre != null)
            persona.Nombre = nombre;

        if (documento != null)
            persona.DocumentoIdentidad = documento;

        if (direccion != null)
            persona.Direccion = direccion;

        if (medioContacto != null)
        {
            persona.MediosDeContacto.Clear();
            persona.MediosDeContacto.Add(medioContacto);
        }

        if (tipoJuridico != null)
            persona.Tipo = tipoJuridico.Value;

        if (rubro != null)
            persona.Rubro = rubro;*/
    }
}