using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class Reportador
{
    private List<Heladera> _heladeras;
    private List<Incidente> _incidentes;
    private List<AccesoHeladera> _accesos;
    private List<Colaborador> _colaboradores;
    public List<IReporteBuilder> ConceptosReporte { get; set; }

    public Reportador(List<Heladera> heladeras, List<Incidente> incidentes, List<AccesoHeladera> accesos,
        List<Colaborador> colaboradores)
    {
        _heladeras = heladeras;
        _incidentes = incidentes;
        _accesos = accesos;
        _colaboradores = colaboradores;
        ConceptosReporte = new List<IReporteBuilder>();
        ConceptosReporte.Add(new ReporteBuilderColaboradorViandasDonadas());
        ConceptosReporte.Add(new ReporteBuilderHeladeraFallas());
        ConceptosReporte.Add(new ReporteBuilderHeladeraCambioViandas());
    }

    public List<Reporte> GenerarReportes(DateTime fechaInicio, DateTime fechaFinal)
    {
        return ConceptosReporte.Select(concepto =>
            concepto.Generar(fechaInicio, fechaFinal, _heladeras, _incidentes, _accesos, _colaboradores)).ToList();
    }
}