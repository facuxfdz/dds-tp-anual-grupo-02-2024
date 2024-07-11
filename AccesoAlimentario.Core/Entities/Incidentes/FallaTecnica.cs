using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public class FallaTecnica
{
    private Colaborador _colaborador;
    private string? _descripcion;
    private string? _foto;
    
    public FallaTecnica(Colaborador colaborador, string? descripcion, string? foto)
    {
        _colaborador = colaborador;
        _descripcion = descripcion;
        _foto = foto;
    }
}