namespace AccesoAlimentario.Core.Entities.Reportes;

public interface IReporteBuilder
{
    Task<Reporte> Generar(DateTime fechaInicio, DateTime fechaFin);
}