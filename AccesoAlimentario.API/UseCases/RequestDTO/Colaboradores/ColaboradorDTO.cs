namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class ColaboradorDTO
{
    public int? Id { get; set; }
    public PersonaDTO? Persona { get; set; }
    
    public ColaboradorDTO(PersonaDTO persona, int? id = null)
    {
        Id = id;
        Persona = persona;
    }
}