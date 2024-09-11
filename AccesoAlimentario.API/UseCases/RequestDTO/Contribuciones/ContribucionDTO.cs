using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Premios;

namespace AccesoAlimentario.API.UseCases.RequestDTO.Contribuciones;

public enum TipoDistribucion
{
    Viandas,
    DonacionVianda,
    OfertaPremio,
    AdministracionHeladera,
    DonacionMonetaria
}

public class ContribucionDTO
{
    public TipoDistribucion Tipo { get; set; }
    public DateTime Fecha { get; set; }
    
    /* DonacionMonetaria */
    public float? DonacionMonetariaMonto { get; set; }
    public int? DonacionMonetariaFrecuenciaDias { get; set; }
    
    /* DistribucionViandas */
    public int? DistribucionViandasHeladeraOrigenId { get; set; }
    public int? DistribucionViandasHeladeraDestinoId { get; set; }
    public int? DistribucionViandasCantViandas { get; set; }
    public MotivoDistribucion? DistribucionViandasMotivoDistribucion { get; set; }
    
    /* DonacionVianda */
    public int? DonacionViandaHeladeraId { get; set; }
    public string? DonacionViandaComida { get; set; }
    public DateTime? DonacionViandaFechaDonacion { get; set; }
    public DateTime? DonacionViandaFechaCaducidad { get; set; }
    public int? DonacionViandaColaboradorId { get; set; }
    public float? DonacionViandaCalorias { get; set; }
    public float? DonacionViandaPeso { get; set; }
    public EstadoVianda? DonacionViandaEstadoVianda { get; set; }
    public int? DonacionViandaUnidadEstandarId { get; set; }
    
    /* OfertaPremio */
    public string? OfertaPremioNombre { get; set; }
    public string? OfertaPremioDescripcion { get; set; }
    public float? OfertaPremioPuntosNecesarios { get; set; }
    public string? OfertaPremioImagen { get; set; } 
    public TipoRubro? OfertaPremioRubro { get; set; }
    
    /* AdministracionHeladera */
    public string? AdministracionHeladeraNombre { get; set; }
    public float? AdministracionHeladeraLongitud { get; set; }
    public float? AdministracionHeladeraLatitud { get; set; }
    public string? AdministracionHeladeraCalle { get; set; }
    public string? AdministracionHeladeraNumero { get; set; }
    public string? AdministracionHeladeraLocalidad { get; set; }
    public string? AdministracionHeladeraPiso { get; set; }
    public string? AdministracionHeladeraDepartamento { get; set; }
    public string? AdministracionHeladeraCodigoPostal { get; set; }
    public DateTime? AdministracionHeladeraFechaInstalacion { get; set; }
    public float? AdministracionHeladeraTemperaturaMinimaConfig { get; set; }
    public float? AdministracionHeladeraTemperaturaMaximaConfig { get; set; }
}