using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores
{
    public class ImportadorColaboraciones
    {
        private readonly FormaImportacion _formaImportacion;
        private GenericRepository<Colaborador> _colaboradorRepository; 

        public ImportadorColaboraciones(FormaImportacion formaImportacion, GenericRepository<Colaborador> colaboradorRepository)
        {
            _formaImportacion = formaImportacion;
            _colaboradorRepository = colaboradorRepository;
        }

        public void Importar(Stream fileStream)
        {
            var colaboradores = _formaImportacion.ImportarColaboradores(fileStream);
            // TODO Buscar colaboradores existentes
            for(int i = 0; i < colaboradores.Count; i++)
            {
                var colaboradorCsv = colaboradores[i];
                 var colaborador = _colaboradorRepository.GetById(colaboradorCsv.Id);
                 if (colaborador == null)
                 {
                     _colaboradorRepository.Insert(colaboradorCsv);
                 }
                 else
                 {
                     colaborador.AgregarPuntos(colaboradorCsv.ObtenerPuntos());
                     _colaboradorRepository.Update(colaborador);
                 }
            }
        }
    }
}