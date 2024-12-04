namespace AccesoAlimentario.Operations.Dto.Responses.MediosContacto;

public abstract class MedioContactoResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public bool Preferida { get; set; } = false;
    public string Tipo { get; set; } = string.Empty;
}