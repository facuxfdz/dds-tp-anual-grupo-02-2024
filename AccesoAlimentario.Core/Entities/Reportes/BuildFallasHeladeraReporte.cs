using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class BuildFallasHeladeraReporte : IReporteBuilder
{
    private readonly IRepository<Heladera> heladerasRepository;
    private readonly IRepository<Incidente> incidentesRepository;
    private readonly IRepository<FallasHeladeraReporteSchema> fallasHeladeraReporteSchemaRepository;
    private readonly IRepository<Reporte> reportesRepository;
    public BuildFallasHeladeraReporte(
        IRepository<Reporte> reportesRepository,
        IRepository<Heladera> heladerasRepository,
        IRepository<Incidente> incidentesRepository,
        IRepository<FallasHeladeraReporteSchema> fallasHeladeraReporteSchemaRepository)
    {
        this.reportesRepository = reportesRepository;
        this.heladerasRepository = heladerasRepository;
        this.incidentesRepository = incidentesRepository;
        this.fallasHeladeraReporteSchemaRepository = fallasHeladeraReporteSchemaRepository;
    }
    public Reporte Build()
    {
        var heladeras = heladerasRepository.Get().ToList();
        if (heladeras.Count == 0)
        {
            throw new Exception("No hay heladeras registradas en el sistema");
        }
        var incidentes = incidentesRepository.Get().ToList();
        var descripcion = "Reporte de fallas en las heladeras";
        var fechaInicio = DateTime.Now.AddDays(-7);
        var fechaFinal = DateTime.Now;
        // el reporte sera valido hasta dentro de una semana
        var validoHasta = DateTime.Now.AddDays(7);
        Console.WriteLine($"Valido hasta: {validoHasta}");
        var incidentesEnIntervalo = incidentes.Where(i => i.Fecha >= fechaInicio && i.Fecha <= fechaFinal).ToList();
        // Ordenamos los incidentes por heladera
        incidentesEnIntervalo.Sort((i1, i2) => i1.Heladera.Id.CompareTo(i2.Heladera.Id));
        // Generamos el reporte a retornar, el id es con el formato yyyy-MM-dd-hh-mm-ss
        string idReporte = $"fallasHeladeras-{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}";
        var reporte = new Reporte(idReporte, descripcion, validoHasta, DateTime.Now);
        try
        {
            reportesRepository.Insert(reporte);
            Console.WriteLine($"Reporte generado con id: {reporte.Id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("El proceso de generacion de reportes fallo");
        }
        // Creamos una instancia de FallasHeladeraReporteSchema por cada incidente de heladera.
        // En caso de que una heladera no tenga incidentes, se crea una instancia con la heladera y la flag de falla en false.
        var datosReporte = new List<FallasHeladeraReporteSchema>();
        foreach (var heladera in heladeras)
        {
            var incidentesHeladera = incidentesEnIntervalo.Where(i => i.Heladera.Id == heladera.Id).ToList();
            if (incidentesHeladera.Count > 0)
            {
                foreach (var incidente in incidentesHeladera)
                {
                    var fallasHeladera = new FallasHeladeraReporteSchema(heladera, true, incidente.Fecha, incidente.Resuelto, reporte);
                    datosReporte.Add(fallasHeladera);
                }
            }
            else
            {
                var fallasHeladera = new FallasHeladeraReporteSchema(heladera, false, null, null, reporte);
                datosReporte.Add(fallasHeladera);
            }
        }

        try
        {
            fallasHeladeraReporteSchemaRepository.InsertMany(datosReporte);
            Console.WriteLine($"Datos de reporte de fallas de heladeras generados correctamente");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("El proceso de generacion de reportes fallo");
        }
        return reporte;
    }
}