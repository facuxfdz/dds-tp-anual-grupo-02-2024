namespace AccesoAlimentario.API.UseCases.RequestDTO.Incentes;

public class FallaTecnicaDTO
{
    public int ColaboradorId { get; set; }
    public int HeladeraId { get; set; }
    public string Descripcion { get; set; } = null!;
    public string Foto { get; set; } = null!;
}