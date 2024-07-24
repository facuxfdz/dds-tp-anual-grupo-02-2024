using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraCambioViandas : IReporteBuilder
{
    public ReporteBuilderHeladeraCambioViandas()
    {
    }

    public Reporte Generar(DateTime fechaInicio, DateTime fechaFinal, List<Heladera> heladeras, List<Incidente> incidentes, List<AccesoHeladera> accesos, List<Colaborador> colaboradores)
    {
        var descripcion = $"Reporte de Viandas Retiradas/Colocadas por Heladera \n Periodo: {fechaInicio.ToString("ddMMyy")} - {fechaFinal.ToString("ddMMyy")}";
        var cuerpo = "Detalle: \n";

        foreach (var heladera in heladeras)
        {
            var donacionesRealizadasEnIntervaloValido = new List<FormaContribucion>();
            foreach (var colaborador in colaboradores)
            {
                var donacionesColaborador = colaborador.ContribucionesRealizadas.Where(c => c.FechaContribucion.Date > fechaInicio.Date && c.FechaContribucion.Date < fechaFinal.Date).ToList();
                donacionesRealizadasEnIntervaloValido = donacionesRealizadasEnIntervaloValido.Concat(donacionesColaborador).ToList();
            }
            
            var viandasDistribuidas = donacionesRealizadasEnIntervaloValido.Where(v => v is DistribucionViandas).ToList();
            var viandasDistribuidasIngreso = new List<DistribucionViandas>();
            var viandasDistribuidasEgreso = new List<DistribucionViandas>();
            foreach(DistribucionViandas distribucion in viandasDistribuidas)
            {
                if(distribucion.HeladeraDestino == heladera)
                    viandasDistribuidasIngreso.Add(distribucion);
                else
                    viandasDistribuidasEgreso.Add(distribucion);
            }

            var viandasDonadas = donacionesRealizadasEnIntervaloValido.Where(v => v is DonacionVianda).Count() + viandasDistribuidasIngreso.Count();
            var viandasSacadas = viandasDistribuidasEgreso.Count();
            
            cuerpo += $"La heladera {heladera.PuntoEstrategico.Nombre} recibió {viandasDonadas} viandas y se retiraron {viandasSacadas} viandas\n";
        }
        return new Reporte(descripcion, cuerpo);
    }
}