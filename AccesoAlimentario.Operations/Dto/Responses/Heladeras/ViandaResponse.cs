using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Heladeras;

public class ViandaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Comida { get; set; } = null!;
    public DateTime FechaDonacion { get; set; } = DateTime.Now;
    public DateTime FechaCaducidad { get; set; } = DateTime.Now;
    public float Calorias { get; set; } = 0;
    public float Peso { get; set; } = 0;
    public EstadoVianda Estado { get; set; } = EstadoVianda.Disponible;
}