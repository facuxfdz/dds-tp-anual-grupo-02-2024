namespace AccesoAlimentario.API.UseCases.RequestDTO.Incentes;

public class VisitaTecnicaDTO
{
    public int IncidenteId { get; set; }
    public int TecnicoId { get; set; }
    public string Foto { get; set; } = null!;
    public string Comentario { get; set; } = null!;
}