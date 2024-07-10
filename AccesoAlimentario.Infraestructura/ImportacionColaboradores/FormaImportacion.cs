using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public abstract class FormaImportacion
{
    public abstract List<Colaborador> ImportarColaboradores(Stream fileStream);
}