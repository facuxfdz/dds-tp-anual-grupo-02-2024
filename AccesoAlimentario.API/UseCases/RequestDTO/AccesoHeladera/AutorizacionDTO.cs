using AccesoAlimentario.API.UseCases.RequestDTO.Colaboradores;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

namespace AccesoAlimentario.API.UseCases.RequestDTO.AccesoHeladera;

public class AutorizacionDTO
{
    public ColaboradorDTO Colaborador { get; set; } = null!;
    public HeladeraDTO Heladera { get; set; } = null!;
}