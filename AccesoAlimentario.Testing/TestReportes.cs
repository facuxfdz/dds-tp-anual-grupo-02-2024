using System.Globalization;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Testing;

// Testear los siguientes casos de uso:
/*
    - Reportes de "fallas de heladeras":
        - Funciona cuando no hay incidentes abiertos en heladeras
    - Reportes de "viandas retiradas/colocadas":
        - Funciona cuando no hay retiros ni colocaciones
    - Reportes de "viandas donadas por colaboradores":
        - Funciona cuando no hay viandas donadas
*/

public class TestReportes
{
    private ReporteBuilderHeladeraFallas _builderHeladeraFallas;
    private ReporteBuilderColaboradorViandasDonadas _builderColaboradorViandasDonadas;
    private ReporteBuilderHeladeraCambioViandas _builderHeladeraCambioViandas;
    private Direccion _direccion1;
    private PuntoEstrategico _punto1;
    private ModeloHeladera _modelo1;
    private Heladera _heladera1;
    private MedioContacto _medioContacto;
    private DocumentoIdentidad _documentoIdentidad1;
    private DocumentoIdentidad _documentoIdentidad2;
    private PersonaHumana _persona1;
    private PersonaJuridica _persona2;
    private Colaborador _colaborador1;
    private Colaborador _colaborador2;
    
    [SetUp]
    public void Setup()
    {
        _builderHeladeraFallas = new ReporteBuilderHeladeraFallas();
        _builderColaboradorViandasDonadas = new ReporteBuilderColaboradorViandasDonadas();
        _builderHeladeraCambioViandas = new ReporteBuilderHeladeraCambioViandas();
        _direccion1 = new Direccion("SiempreViva", "123", "Lomas", "1234");
        _punto1 = new PuntoEstrategico("heladera lomas", 9.21f, 2.1f, _direccion1);
        _modelo1 = new ModeloHeladera(20, -15, 5);
        _heladera1 = new Heladera(_punto1, -10f, 5f, _modelo1);
        _medioContacto = new Email(true, "direccionfalsa@gmail.com");
        _documentoIdentidad1 = new DocumentoIdentidad(TipoDocumento.DNI, 214214515, DateOnly.Parse("February 1, 1999", CultureInfo.InvariantCulture));
        _documentoIdentidad2 = new DocumentoIdentidad(TipoDocumento.CUIL, 2145345343, DateOnly.Parse("February 1, 1999", CultureInfo.InvariantCulture));
        _persona1 = new PersonaHumana("Pepito", "Terrabusi", [_medioContacto] , _direccion1, _documentoIdentidad1, SexoDocumento.Otro);
        _colaborador1 = new Colaborador(_persona1, [TipoContribucion.DINERO]);
        _persona2 = new PersonaJuridica("Terrabusi", "SA", TipoJuridico.Empresa, "Alimentos", [_medioContacto], _direccion1, _documentoIdentidad2);
        _colaborador2 = new Colaborador(_persona2, [TipoContribucion.DINERO]);
    }
    
    [Test]
    public void ReporteHeladeraFallasFuncionaSinIncidentes()
    {
        Assert.IsInstanceOf<Reporte>(_builderHeladeraFallas.Generar(DateTime.Now.AddDays(-1), DateTime.Now, [_heladera1], [], [], []));
    }
    
    [Test]
    public void ReporteHeladeraCambioViandasFuncionaSinCambios()
    {
        Assert.IsInstanceOf<Reporte>(_builderHeladeraCambioViandas.Generar(DateTime.Now.AddDays(-1), DateTime.Now, [_heladera1], [], [], [_colaborador1]));
    }
    
    [Test]
    public void ReporteColaboradorViandasDonadas()
    {
        Assert.IsInstanceOf<Reporte>(_builderColaboradorViandasDonadas.Generar(DateTime.Now.AddDays(-1), DateTime.Now, [_heladera1], [], [], [_colaborador1,_colaborador2]));
    }
}