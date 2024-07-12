using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Servicios;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorColaboraciones
{
    private readonly FormaImportacion _formaImportacion;
    private UnitOfWork _unitOfWork;
    private ColaboracionesServicio _colaboracionesServicio;

    public ImportadorColaboraciones(FormaImportacion formaImportacion, UnitOfWork unitOfWork, ColaboracionesServicio colaboracionesServicio)
    {
        _formaImportacion = formaImportacion;
        _unitOfWork = unitOfWork;
        _colaboracionesServicio = colaboracionesServicio;
    }
    public void Importar(Stream fileStream)
        {
            var colaboradores = _formaImportacion.ImportarColaboradores(fileStream);
            // TODO Buscar colaboradores existentes
            for(int i = 0; i < colaboradores.Count; i++)
            {
                var colaboradorCsv = colaboradores[i];
                 var colaborador = _unitOfWork.ColaboradorRepository.GetById(colaboradorCsv.Id);
                 if (colaborador == null)
                 {
                     _unitOfWork.ColaboradorRepository.Insert(colaboradorCsv);
                     foreach (var contribucion in colaboradorCsv.ObtenerContribuciones())
                     {
                         _colaboracionesServicio.cargarColaboracionCsv(contribucion,colaboradorCsv);
                     }
                     Console.WriteLine("Colaborador insertado");
                     Console.WriteLine(colaboradorCsv);
                 }
                 else
                 {
                     colaborador.AgregarPuntos(colaboradorCsv.ObtenerPuntos());
                     _unitOfWork.ColaboradorRepository.Update(colaborador);
                 }
            }
        }
}