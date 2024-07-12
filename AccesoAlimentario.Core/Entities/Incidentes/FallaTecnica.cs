using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class FallaTecnica : Incidente
{
    public Colaborador _colaborador = null!;
    public string? _descripcion = null!;
    public string? _foto = null!;

    public FallaTecnica()
    {
    }

    public FallaTecnica(Colaborador colaborador, string? descripcion, string? foto)
    {
        _colaborador = colaborador;
        _descripcion = descripcion;
        _foto = foto;
    }
}