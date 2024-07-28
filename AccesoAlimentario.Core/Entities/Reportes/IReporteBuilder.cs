using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Reportes;

public interface IReporteBuilder
{
    public Reporte Build();

}