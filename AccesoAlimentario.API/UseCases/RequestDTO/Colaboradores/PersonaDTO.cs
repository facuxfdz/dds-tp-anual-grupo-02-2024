namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class PersonaDTO
{
    public int Id { get; set; }
    
    public PersonaDTO(int id)
    {
        Id = id;
    }
    
}