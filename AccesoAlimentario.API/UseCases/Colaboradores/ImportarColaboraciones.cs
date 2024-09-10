using System.Globalization;
using System.Linq.Expressions;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.UseCases.RequestDTO.ImportacionColaboraciones;
using CsvHelper;

namespace AccesoAlimentario.API.UseCases.Colaboradores;

public class ImportarColaboraciones(IRepository<Colaborador> colaboradorRepository)
{
    private static List<string> _tiposDocumento = ["DNI", "LE", "LC", "CUIL", "CUIT"];
    private static List<string> _tiposContribucion =
        ["DINERO", "DONACION_VIANDAS", "REDISTRIBUCION_VIANDAS", "ENTREGA_TARJETAS"];

    private List<ColaboracionCSVDTO> _leerCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<ColaboracionCSVDTO>().ToList();
    }
    
    private float _calcularPuntos()
    {
        return 0;
    }
    private bool _between(float min, float max, float value)
    {
        return value > min && value <= max;
    }
    private List<ColaboracionCSVDTO> _validarColaboraciones(List<ColaboracionCSVDTO> colaboradores)
    {
        return colaboradores.Where(colaborador =>
            _tiposDocumento.Contains(colaborador.TipoDocumento)
            && colaborador.Documento.Length > 0
            && _between(0, 50, colaborador.Nombre.Length)
            && _between(0, 50, colaborador.Apellido.Length)
            && _between(0, 50, colaborador.Mail.Length)
            && colaborador.Mail.Contains("@")
            && !colaborador.Mail.Last().ToString().Equals("@")
            && DateOnly.TryParseExact(colaborador.FechaColaboracion, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
            && _tiposContribucion.Contains(colaborador.FormaColaboracion)
            && _between(0, 9999999, colaborador.Cantidad)
        ).ToList();
    }
    
    private List<Contribucion> _crearContribuciones(ColaboracionCSVDTO colaboradorCsv)
    {
        if(colaboradorCsv.FormaColaboracion == "REDISTRIBUCION_VIANDAS")
        {
            colaboradorCsv.FormaColaboracion = TipoContribucion.DISTRIBUCION_VIANDAS.ToString();
        }
        TipoContribucion tipoContribucion = Enum.Parse<TipoContribucion>(colaboradorCsv.FormaColaboracion);
        List<Contribucion> contribuciones = new List<Contribucion>();
        DateTime fechaColaboracion = DateTime.ParseExact(colaboradorCsv.FechaColaboracion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        switch (tipoContribucion)
        {
            case TipoContribucion.MONETARIA:
                Console.WriteLine("Donacion monetaria no implementada");
                // TODO: Implementar DonacionMonetaria
                break;
            case TipoContribucion.DONACION_VIANDAS:
                Console.WriteLine("Donacion de viandas no implementada");
                // TODO: Implementar DonacionVianda
                break;
            case TipoContribucion.DISTRIBUCION_VIANDAS: 
                contribuciones.Add(new DistribucionViandas(fechaColaboracion, null, null, colaboradorCsv.Cantidad));
                break;
            case TipoContribucion.ENTREGA_TARJETAS:
                Console.WriteLine("Entrega de tarjetas no implementada");
                // TODO: Implementar EntregaTarjeta
                break;
        }

        return contribuciones;
    }
    
    private Colaborador _parseColaborador(ColaboracionCSVDTO colaboradorCsv)
    {
        colaboradorCsv.TipoDocumento = colaboradorCsv.TipoDocumento.ToUpper();
        TipoDocumento tipoDocumento = Enum.Parse<TipoDocumento>(colaboradorCsv.TipoDocumento);
        DocumentoIdentidad documentoIdentidad = new DocumentoIdentidad(tipoDocumento, colaboradorCsv.Documento, DateOnly.MinValue);
        Persona persona = new PersonaHumana(colaboradorCsv.Nombre, colaboradorCsv.Apellido ,null, documentoIdentidad, SexoDocumento.Otro);
        Colaborador colaborador = new Colaborador(persona);
        // UsuarioSistema usuario = new UsuarioSistema(persona, colaboradorCsv.Mail, CrearPassword());
        // persona.AgregarRol(usuario);
        // persona.AgregarRol(colaborador);
        // TODO: Crear la clase UsuarioSistema y actualizar aquí
        List<Contribucion> contribuciones = _crearContribuciones(colaboradorCsv);

        foreach (var contribucion in contribuciones)
        {
            colaborador.AgregarContribucion(contribucion);
        }

        return colaborador;
    }
    
    public void ImportarCsv(Stream fileStream)
    {
        if (fileStream == null)
        {
            throw new Exception("Error al importar los colaboradores. El archivo no pudo ser leído.");
        }
        List<ColaboracionCSVDTO> colaboradores;
        try
        {
            colaboradores = _leerCsv(fileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al importar los colaboradores. El archivo no pudo ser leído.");
        }
        List<ColaboracionCSVDTO> colaboradoresValidos = _validarColaboraciones(colaboradores);
        if(colaboradoresValidos.Count == 0)
        {
            throw new Exception("Error al importar los colaboradores. El archivo contiene datos inválidos.");
        }

        foreach (var colaboradorCsv in colaboradoresValidos)
        {
            Expression<Func<Colaborador, bool>> filter = x =>
                x.Persona.DocumentoIdentidad!.NroDocumento == colaboradorCsv.Documento;
            var colaborador = colaboradorRepository.Get(filter).FirstOrDefault();

            if (colaborador == null)
            {
                colaborador = _parseColaborador(colaboradorCsv);
                colaboradorRepository.Insert(colaborador);
                Console.WriteLine("Colaborador insertado");
                Console.WriteLine(colaborador);
            }
            else
            {
                colaborador.AgregarPuntos(_calcularPuntos());
                colaboradorRepository.Update(colaborador);
            }
        }
    }
    
}