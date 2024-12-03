using AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;

namespace AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

public abstract class TarjetaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Codigo { get; set; } = null!;
    public List<AccesoHeladeraResponse> Accesos { get; set; } = null!;
    public string Tipo { get; set; } = null!;
}