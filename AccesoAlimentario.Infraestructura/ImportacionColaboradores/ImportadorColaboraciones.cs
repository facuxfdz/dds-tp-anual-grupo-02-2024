using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorColaboraciones
{
    private readonly FormaImportacion _formaImportacion;
    private UnitOfWork _unitOfWork; 

    public ImportadorColaboraciones(FormaImportacion formaImportacion, UnitOfWork unitOfWork)
    {
        _formaImportacion = formaImportacion;
        _unitOfWork = unitOfWork;
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