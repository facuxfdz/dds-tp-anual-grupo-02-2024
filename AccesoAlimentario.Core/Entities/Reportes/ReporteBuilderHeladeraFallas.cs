using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class ReporteBuilderHeladeraFallas : IReporteBuilder{
   
    public ReporteBuilderHeladeraFallas()
    {
    }

        public Reporte Generar(DateTime fechaInicio, DateTime fechaFinal, List<Heladera> heladeras, List<Incidente> incidentes, List<AccesoHeladera> accesos, List<Colaborador> colaboradores)
    {
        var descripcion = $"Reporte de fallas en las heladeras \n Periodo: {fechaInicio.ToString("ddMMyy")} - {fechaFinal.ToString("ddMMyy")}";
        var cuerpo = "Detalle: \n";

        foreach (var heladera in heladeras)
        {
          var fallasEnIntervaloValido = heladera.Incidentes.Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFinal).Count();
            if (fallasEnIntervaloValido > 0)
                cuerpo += $"Heladera: {heladera.PuntoEstrategico.Nombre} tuvo {fallasEnIntervaloValido} fallas\n";
        }

        return new Reporte(descripcion, cuerpo);
    }
}