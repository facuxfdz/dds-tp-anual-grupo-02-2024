using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class VisitaTecnica
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Tecnico Tecnico { get; set; } = null!;
    public string? Foto { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string? Comentario { get; set; } = null!;

    public VisitaTecnica()
    {
    }

    public VisitaTecnica(Tecnico tecnico, string? foto, string? comentario)
    {
        Tecnico = tecnico;
        Foto = foto;
        Fecha = DateTime.Now;
        Comentario = comentario;
    }
    
    public VisitaTecnica(Tecnico tecnico, string? foto, DateTime fecha, string? comentario)
    {
        Tecnico = tecnico;
        Foto = foto;
        Fecha = fecha;
        Comentario = comentario;
    }
}