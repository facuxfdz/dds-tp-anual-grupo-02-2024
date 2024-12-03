using AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;

namespace AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

public class TarjetaColaboracionResponse : TarjetaResponse
{
    public List<AutorizacionManipulacionHeladeraResponse> Autorizaciones { get; set; } = [];
}