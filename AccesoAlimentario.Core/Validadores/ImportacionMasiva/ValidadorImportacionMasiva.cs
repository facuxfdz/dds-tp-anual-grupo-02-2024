using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Validadores.ImportacionMasiva;

public class ValidadorImportacionMasiva
{
    public bool Validar(string tipoDoc, int documento, string nombre, string apellido, string mail,
        string fechaColaboracion, string formaColaboracion, int cantidad)
    {
        return tipoDoc.Length > 0
               && tipoDoc.Length <= 3
               && (string.Equals(tipoDoc, "DNI") || string.Equals(tipoDoc, "LC") || string.Equals(tipoDoc, "LE"))
               && documento > 0 && documento <= 9999999999
               && nombre.Length > 0 && nombre.Length <= 50
               && apellido.Length > 0 && apellido.Length <= 50
               && mail.Length > 0 && mail.Length <= 50 && mail.Contains("@")
               && DateOnly.TryParse(fechaColaboracion, out _)
               && formaColaboracion.Length > 0 && formaColaboracion.Length <= 22
               && (string.Equals(formaColaboracion, "DINERO")
                   || string.Equals(formaColaboracion, "DONACION_VIANDAS")
                   || string.Equals(formaColaboracion, "REDISTRIBUCION_VIANDAS")
                   || string.Equals(formaColaboracion, "ENTREGA_TARJETAS"))
               && cantidad > 0 && cantidad <= 9999999;
    }
}