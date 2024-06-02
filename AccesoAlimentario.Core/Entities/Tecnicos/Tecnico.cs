using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;

namespace AccesoAlimentario.Core.Entities.Tecnicos;

public class Tecnico
{
    private string _nombre;
    private string _apellido;
    private DocumentoIdentidad _documentoIdentidad;
    private int _cuil;
    private MedioContacto _medioContacto;
    private AreaCobertura _areaCobertura;
    
    public Tecnico(string nombre, string apellido, DocumentoIdentidad documentoIdentidad, int cuil, MedioContacto medioContacto, AreaCobertura areaCobertura)
    {
        _nombre = nombre;
        _apellido = apellido;
        _documentoIdentidad = documentoIdentidad;
        _cuil = cuil;
        _medioContacto = medioContacto;
        _areaCobertura = areaCobertura;
    }
    
    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        _areaCobertura = areaCobertura;
    }
}