using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Beneficiarios;

public class PersonaVulnerable
{
    private string _nombre;
    // private DateOffset _fechaNacimiento;
    // private DateOffset _fechaRegistroSistema;
    private Direccion _direccion;
    private TipoDocumento _tipoDocumento;
    private int _nroDocumento;
    private int _cantidadDeMenores;

}