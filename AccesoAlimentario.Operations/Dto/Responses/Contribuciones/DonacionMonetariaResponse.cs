namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class DonacionMonetariaResponse : FormaContribucionResponse
{
    public float Monto { get; set; } = 0;
    public int FrecuenciaDias { get; set; } = 0;
}