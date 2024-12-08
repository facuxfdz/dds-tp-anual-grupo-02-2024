using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class FallaTecnica : Incidente
{
    public virtual Colaborador? Colaborador { get; set; } = null;
    public string? Descripcion { get; set; } = null;
    public string? Foto { get; set; } = null;

    public FallaTecnica()
    {
    }

    public FallaTecnica(Colaborador colaborador, string? descripcion, string? foto)
    {
        Colaborador = colaborador;
        Descripcion = descripcion;
        Foto = foto;
    }
}