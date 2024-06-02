using System.Runtime.InteropServices.JavaScript;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ParserDatosColaboracion
{
    public void pasearDatos(DatosColaboracion datos)
    {
        //TODO persistencia
        //TODO contribuciones
        var tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), datos.TipoDoc);
        var documento = new DocumentoIdentidad(tipoDoc, datos.Documento, SexoDocumento.Otro);
        var persona = new PersonaHumana(datos.Nombre, datos.Apellido, null, null);
    }
    
}