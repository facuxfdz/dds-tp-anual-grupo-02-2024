using System.Linq.Expressions;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;

public class ImportadorColaboraciones
{
    private readonly FormaImportacion _formaImportacion;
    private GenericRepository<Colaborador> _colaboradorRepository;

    public ImportadorColaboraciones(FormaImportacion formaImportacion,
        GenericRepository<Colaborador> colaboradorRepository)
    {
        _formaImportacion = formaImportacion;
        _colaboradorRepository = colaboradorRepository;
    }

    public void Importar(Stream fileStream)
    {
        var colaboradores = _formaImportacion.ImportarColaboradores(fileStream);
        for (int i = 0; i < colaboradores.Count; i++)
        {
            var colaboradorCsv = colaboradores[i];
            Expression<Func<Colaborador, bool>> filter = x =>
                x.Persona.DocumentoIdentidad.NroDocumento == colaboradorCsv.Persona.DocumentoIdentidad.NroDocumento;
            var colaborador = _colaboradorRepository.Get(filter).FirstOrDefault();

            if (colaborador == null)
            {
                _colaboradorRepository.Insert(colaboradorCsv);
                Console.WriteLine("Colaborador insertado");
                Console.WriteLine(colaboradorCsv);
            }
            else
            {
                colaborador.AgregarPuntos(colaboradorCsv.ObtenerPuntos());
                _colaboradorRepository.Update(colaborador);
            }
        }
    }
}