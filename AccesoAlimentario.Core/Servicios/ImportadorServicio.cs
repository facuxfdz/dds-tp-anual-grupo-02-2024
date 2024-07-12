using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;

namespace AccesoAlimentario.Core.Servicios;

public class ImportadorServicio(UnitOfWork unitOfWork, PersonasServicio personasServicio, ColaboradoresServicio colaboradoresServicio)
{
    public void Importar(string archivoBase64)
    {
        var importador = new ImportadorCsv();
        using var streamFile = new MemoryStream(Convert.FromBase64String(archivoBase64));
        var colaboradores = importador.ImportarColaboradores(streamFile);
        foreach (var colaborador in colaboradores)
        {
            var persona = (PersonaHumana)colaborador.Persona;
            var existePersona = personasServicio.Buscar(persona.DocumentoIdentidad);
            if (existePersona == null)
            {
                persona = personasServicio.CrearHumana(persona.Nombre, persona.Direccion, persona.DocumentoIdentidad, persona.MediosDeContacto, persona.Apellido, persona.Sexo);
            } else {
                persona = (PersonaHumana)existePersona;
            }
            // TODO: ACA SE DEBERIA VALIDAR SI POSEE EL ROL DE COLABORADOR
            colaboradoresServicio.Crear(persona, []);
            /*colaborador.ContribucionesRealizadas.ForEach(contribucion =>
            {
                switch (contribucion)
                {
                    
                }
            });*/
        }
    }
}