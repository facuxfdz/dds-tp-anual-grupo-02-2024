using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Colaboraciones.Suscripciones;

public abstract class EventoHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public List<Colaborador> Suscriptores { get; private set; }
    
    public EventoHeladera(){}
    
    public EventoHeladera(List<Colaborador> suscriptores)
    {
        this.Suscriptores = suscriptores;
    }
    
    public void SuscribirColaborador(Colaborador colaborador)
    {
        this.Suscriptores.Add(colaborador);
    }
}