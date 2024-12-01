using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Operations.Dto.Responses.Contribuciones;
using AccesoAlimentario.Operations.Dto.Responses.SuscripcionesColaboradores;
using AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

namespace AccesoAlimentario.Operations.Dto.Responses.Roles;

public class ColaboradorResponse : RolResponse
{
    public List<TipoContribucion> ContribucionesPreferidas { get; set; } = [];
    public List<FormaContribucionResponse> ContribucionesRealizadas { get; set; } = [];
    public List<SuscripcionResponse> Suscripciones { get; set; } = [];
    public float Puntos { get; set; } = 0;
    public TarjetaColaboracionResponse? TarjetaColaboracion { get; set; } = null!;
}