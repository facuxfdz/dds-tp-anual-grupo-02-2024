using System.Globalization;

namespace AccesoAlimentario.Core.Validadores.ImportacionMasiva;

public class ValidadorImportacionMasiva
{
    private static List<string> _tiposContribucion =
        ["DINERO", "DONACION_VIANDAS", "REDISTRIBUCION_VIANDAS", "ENTREGA_TARJETAS"];

    private static List<string> _tiposDocumento = ["DNI", "LE", "LC"]; // por qué no "CUIL", "CUIT" ?

    public bool Validar(string tipoDoc, int documento, string nombre, string apellido, string mail,
        string fechaColaboracion, string formaColaboracion, int cantidad)
    {
        return _tiposDocumento.Contains(tipoDoc)
               && SeEncuentraEntre(0, 9999999999, documento)
               && SeEncuentraEntre(0, 50, nombre.Length)
               && SeEncuentraEntre(0, 50, apellido.Length)
               && SeEncuentraEntre(0, 50, mail.Length) && mail.Contains("@") && !mail.Last().ToString().Equals("@")
               && DateTime.TryParseExact(fechaColaboracion, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
               && _tiposContribucion.Contains(formaColaboracion)
               && SeEncuentraEntre(0, 9999999, cantidad);
    }

    private static bool SeEncuentraEntre(float minimo, float maximo, float valor)
    {
        return valor > minimo && valor <= maximo;
    }
}