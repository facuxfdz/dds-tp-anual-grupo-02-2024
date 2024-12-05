namespace AccesoAlimentario.Operations.Dto.Responses.Externos;

public class ColaboradorResponseExterno
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Nombre { get; set; } = string.Empty;
    public float Puntos { get; set; } = 0;
    public int DonacionesUltimoMes { get; set; } = 0;
}
