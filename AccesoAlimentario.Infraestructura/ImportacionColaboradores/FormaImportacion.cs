using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public abstract class FormaImportacion
{
    public abstract List<Colaborador> ImportarColaboradores(Stream fileStream);
}