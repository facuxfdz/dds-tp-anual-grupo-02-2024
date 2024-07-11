using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Personas.Tecnicos;

namespace AccesoAlimentario.Core.Servicios;

public class TecnicosServicio
{
    public void Crear(Persona persona, float latitud, float longitud, float radio)
    {

    }

    public void Eliminar(Tecnico tecnico)
    {

    }

    public void Modificar(Tecnico tecnico, float latitud, float longitud, float radio)
    {

    }

    public ICollection<Tecnico> ObtenerTecnicosEnZona(float latitud, float longitud)
    {
        return new List<Tecnico>();
    }
}