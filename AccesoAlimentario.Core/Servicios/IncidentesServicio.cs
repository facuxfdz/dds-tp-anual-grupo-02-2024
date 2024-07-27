using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;

namespace AccesoAlimentario.Core.Servicios;

public class IncidentesServicio(UnitOfWork unitOfWork)
{
    public void CrearIncidente(int idHeladera, Incidente incidente)
    {
        var heladera = unitOfWork.HeladeraRepository.GetById(idHeladera);
        
        if (heladera == null)
            throw new Exception("Heladera no encontrada");
        
        heladera.Incidentes.Add(incidente);
        unitOfWork.HeladeraRepository.Update(heladera);
    }

    public void NotificarVisita(int idIncidente, int idTecnico, string foto, DateTime fecha, string comentarios)
    {
        var incidente = unitOfWork.IncidenteRepository.GetById(idIncidente);
        var tecnico = unitOfWork.TecnicoRepository.GetById(idTecnico);
        
        if (incidente == null)
            throw new Exception("Incidente no encontrado");
        
        if (tecnico == null)
            throw new Exception("Tecnico no encontrado");

        var visita = new VisitaTecnica(tecnico, foto, fecha, comentarios);
        
        incidente.VisitasTecnicas.Add(visita);
        unitOfWork.IncidenteRepository.Update(incidente);
        
        unitOfWork.VisitaTecnicaRepository.Insert(visita);
    }
}