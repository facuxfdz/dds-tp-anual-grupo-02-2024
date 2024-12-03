using AccesoAlimentario.Core.Entities.Premios;

namespace AccesoAlimentario.Operations.Dto.Responses.Premios;

public class PremioResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Nombre { get; set; } = null!;
    public float PuntosNecesarios { get; set; } = 0;
    public string Imagen { get; set; } = null!;
    public DateTime FechaReclamo { get; set; } = DateTime.MinValue;
    public TipoRubro Rubro { get; set; } = TipoRubro.Otros;
}