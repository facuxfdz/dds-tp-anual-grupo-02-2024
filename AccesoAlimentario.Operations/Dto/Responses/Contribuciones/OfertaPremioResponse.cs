using AccesoAlimentario.Operations.Dto.Responses.Premios;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class OfertaPremioResponse : FormaContribucionResponse
{
    public PremioResponse Premio { get; set; } = null!;
}