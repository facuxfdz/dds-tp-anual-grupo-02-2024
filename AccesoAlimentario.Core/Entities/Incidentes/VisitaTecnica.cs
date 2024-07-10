using AccesoAlimentario.Core.Entities.Personas.Tecnicos;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class VisitaTecnica
{
    private Tecnico _tecnico;
    private string? _foto;
    private DateTime _fecha;
    private string? _comentario;
    
    public VisitaTecnica(Tecnico tecnico, string? foto, string? comentario)
    {
        _tecnico = tecnico;
        _foto = foto;
        _fecha = DateTime.Now;
        _comentario = comentario;
    }
}