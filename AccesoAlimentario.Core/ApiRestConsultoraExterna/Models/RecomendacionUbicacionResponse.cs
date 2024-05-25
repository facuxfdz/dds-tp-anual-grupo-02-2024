using Newtonsoft.Json;

namespace ApiRestConsultoraExterna.Models;

public class DatosResponse
{
    public string Longitud { get; set; }
    public string Latitud { get; set; }
    public DireccionResponse Direccion { get; set; }
}

public class DireccionResponse
{
    public string Calle { get; set; }
    public string Numero { get; set; }
    public string Localidad { get; set; }
    public string CodigoPostal { get; set; }
}

public class RecomendacionesUbicacionResponse
{
    public List<DatosResponse> data { get; set; }
}

