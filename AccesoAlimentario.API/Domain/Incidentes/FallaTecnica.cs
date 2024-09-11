using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class FallaTecnica
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public Colaborador Colaborador = null!;
    public string Descripcion = string.Empty;
    public string Foto = string.Empty;
    
    public FallaTecnica()
    {
    }

    public FallaTecnica(DateTime fecha, Heladera heladera, Colaborador colaborador, string descripcion, string foto)
    {
        Colaborador = colaborador;
        Descripcion = descripcion;
        Foto = foto;
    }
}