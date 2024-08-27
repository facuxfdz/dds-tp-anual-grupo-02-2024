using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.UseCases.RequestDTO.AccesoHeladera;

public class AccesoHeladeraDTO
{
    public int TarjetaId { get; set; }
    public int HeladeraId { get; set; }
    public TipoAcceso TipoAcceso { get; set; }
    public int? AutorizacionId { get; set; }
}