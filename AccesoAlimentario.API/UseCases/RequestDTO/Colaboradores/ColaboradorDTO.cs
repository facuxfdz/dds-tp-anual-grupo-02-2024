namespace AccesoAlimentario.API.UseCases.RequestDTO.Colaboradores;

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