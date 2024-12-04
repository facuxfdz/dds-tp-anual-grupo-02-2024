namespace AccesoAlimentario.Operations.Dto.Responses.Externos;

public class ColaboradorResponseExterno
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public float Puntos { get; set; }
    public int DonacionesUltimoMes { get; set; }
}
