using System.Globalization;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.ImportacionMasiva;
using CsvHelper;

namespace AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores
{
    public class ImportadorColaboraciones
{
    private readonly ValidadorImportacionMasiva _validador;
    /*private readonly IRepository<Colaborador> _colaboradorRepository;*/

    /*public ImportadorColaboraciones(IRepository<Colaborador> colaboradorRepository, ValidadorImportacionMasiva validador)
    {
        _colaboradorRepository = colaboradorRepository;
        _validador = validador;
    }*/

    /*public void Importar(Stream? fileStream)
    {
        if (fileStream == null)
        {
            throw new Exception("Error al importar los colaboradores. El archivo no pudo ser leído.");
        }
        List<DatosColaboracion> colaboradores;
        try
        {
            colaboradores = LeerCsv(fileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al importar los colaboradores. El archivo no pudo ser leído.");
        }
        List<DatosColaboracion> colaboradoresValidos = ValidarColaboradores(colaboradores);
        if(colaboradoresValidos.Count == 0)
        {
            throw new Exception("Error al importar los colaboradores. El archivo contiene datos inválidos.");
        }

        foreach (var colaboradorCsv in colaboradoresValidos)
        {
            Expression<Func<Colaborador, bool>> filter = x =>
                x.Persona.DocumentoIdentidad!.NroDocumento == colaboradorCsv.Documento;
            var colaborador = _colaboradorRepository.Get(filter).FirstOrDefault();

            if (colaborador == null)
            {
                colaborador = Parsear(colaboradorCsv);
                _colaboradorRepository.Insert(colaborador);
                Console.WriteLine("Colaborador insertado");
                Console.WriteLine(colaborador);
            }
            else
            {
                colaborador.AgregarPuntos(CalcularPuntos(colaboradorCsv));
                _colaboradorRepository.Update(colaborador);
            }
        }
    }*/

    public List<DatosColaboracion> LeerCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<DatosColaboracion>().ToList();
        return records;
    }

    public List<DatosColaboracion> ValidarColaboradores(List<DatosColaboracion> colaboradores)
    {
        return colaboradores.Where(c =>
            _validador.Validar(c.TipoDoc, c.Documento, c.Nombre, c.Apellido, c.Mail, c.FechaColaboracion,
                c.FormaColaboracion, c.Cantidad)).ToList();
    }

    public Colaborador Parsear(DatosColaboracion datos)
    {
        var tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), datos.TipoDoc);
        var documento = new DocumentoIdentidad(tipoDoc, datos.Documento, DateTime.MinValue);
        var personaHumana = new PersonaHumana(datos.Nombre, datos.Apellido, new List<MedioContacto>
        {
            new Email(true, datos.Mail)
        }, null, documento, SexoDocumento.Otro);
        var colaborador = new Colaborador(personaHumana, []);
        var usuario = new UsuarioSistema(personaHumana, datos.Mail, CrearPassword(), "", RegisterType.BulkImport);
        personaHumana.AgregarRol(usuario);
        personaHumana.AgregarRol(colaborador);

        var contribuciones = CrearContribuciones(datos);

        foreach (var contribucion in contribuciones)
        {
            colaborador.AgregarContribucion(contribucion);
        }

        return colaborador;
    }

    public List<FormaContribucion> CrearContribuciones(DatosColaboracion datos)
    {
        var tipoContribucion = (TipoContribucion)Enum.Parse(typeof(TipoContribucion), datos.FormaColaboracion);
        var contribuciones = new List<FormaContribucion>();
        var date = DateTime.ParseExact(datos.FechaColaboracion, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (tipoContribucion)
        {
            case TipoContribucion.DINERO:
                contribuciones.Add(new DonacionMonetaria(date, datos.Cantidad, 0));
                break;
            case TipoContribucion.DONACION_VIANDAS:
                for (var i = 0; i < datos.Cantidad; i++)
                {
                    contribuciones.Add(new DonacionVianda(date, null, null));
                }
                break;
            case TipoContribucion.REDISTRIBUCION_VIANDAS:
                contribuciones.Add(new DistribucionViandas(date, null, null, datos.Cantidad, MotivoDistribucion.Desperfecto));
                break;
            case TipoContribucion.ENTREGA_TARJETAS:
                for (var i = 0; i < datos.Cantidad; i++)
                {
                    contribuciones.Add(new RegistroPersonaVulnerable(date, null));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return contribuciones;
    }

    public int CalcularPuntos(DatosColaboracion colaboradorCsv)
    {
        // Implementa la lógica de cálculo de puntos según tu lógica de negocio
        return 0;
    }

    public string CrearPassword()
    {
        const int length = 16;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

}
