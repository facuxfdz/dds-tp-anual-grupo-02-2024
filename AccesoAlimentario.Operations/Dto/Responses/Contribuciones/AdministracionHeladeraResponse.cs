using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class AdministracionHeladeraResponse : FormaContribucionResponse
{
    public HeladeraResponse Heladera { get; set; } = null!;
}