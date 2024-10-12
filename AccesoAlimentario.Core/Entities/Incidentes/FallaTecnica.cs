using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class FallaTecnica : Incidente
{
    public Colaborador Colaborador = null!;
    public string? Descripcion = null;
    public string? Foto = null;

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