using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;

namespace AccesoAlimentario.API.UseCases.Personas;

public class CrearPersona(IRepository<Persona> repositorioPersona)
{
    private bool _validarPersonaHumana(PersonaDTO persona)
    {
        return persona.Apellido != null && persona.Sexo != null;
    }
    private bool _validarPersonaJuridica(PersonaDTO persona)
    {
        return persona.RazonSocial != null && persona.TipoJuridico != null && persona.Rubro != null;
    }
    private void _crearPersonaHumana(string nombre, Direccion direccion, DocumentoIdentidad documentoIdentidad, PersonaDTO persona)
    {
        if (!_validarPersonaHumana(persona))
        {
            throw new RequestInvalido("Persona humana tiene campos incorrectos o nulos");
        }
        var apellido = persona.Apellido!;
        SexoDocumento sexo = (SexoDocumento)Enum.Parse(typeof(SexoDocumento), persona.Sexo!);
        var personaACrear = new PersonaHumana(nombre, apellido, direccion, documentoIdentidad, sexo);

        repositorioPersona.Insert(personaACrear);
    }
    
    private void _crearPersonaJuridica(string nombre, Direccion direccion, DocumentoIdentidad documentoIdentidad, PersonaDTO persona)
    {
        if (!_validarPersonaJuridica(persona))
        {
            throw new RequestInvalido("Persona juridica tiene campos incorrectos o nulos");
        }
        var razonSocial = persona.RazonSocial;
        TipoJuridico tipo = (TipoJuridico)persona.TipoJuridico!;
        var rubro = persona.Rubro;
        var personaACrear = new PersonaJuridica(nombre, razonSocial, tipo, rubro, direccion, documentoIdentidad);

        repositorioPersona.Insert(personaACrear);
    }
    public void Crear(PersonaDTO persona)
    {
        var nombre = persona.Nombre!;
        var direccion = new Direccion(persona.Direccion!.Calle, persona.Direccion.Numero, persona.Direccion.Localidad, persona.Direccion.CodigoPostal);
        var documento = new DocumentoIdentidad(
            persona.DocumentoIdentidad!.Tipo,
            persona.DocumentoIdentidad.Numero,
            persona.DocumentoIdentidad.FechaVencimiento
        );
        if(persona.TipoPersona == TipoPersona.JURIDICA.ToString())
        {
            _crearPersonaJuridica(nombre, direccion, documento, persona);
        }
        else if (persona.TipoPersona == TipoPersona.HUMANA.ToString())
        {
            _crearPersonaHumana(nombre, direccion, documento, persona);
        }
        else
        {
         throw new TipoDePersonaInvalido();   
        }
        
    }
}