using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class DonacionViandaResponse : FormaContribucionResponse
{
    public HeladeraResponse Heladera { get; set; } = null!;
    public ViandaResponse Vianda { get; set; } = null!;
}