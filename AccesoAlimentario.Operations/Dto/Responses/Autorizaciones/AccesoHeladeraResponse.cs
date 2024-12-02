﻿using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;

public class AccesoHeladeraResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public List<ViandaResponse> Viandas { get; set; } = null!;
    public DateTime FechaAcceso { get; set; } = DateTime.Now;
    public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    public HeladeraResponse Heladera { get; set; } = null!;
    public AutorizacionManipulacionHeladeraResponse? Autorizacion { get; set; } = null!;
}