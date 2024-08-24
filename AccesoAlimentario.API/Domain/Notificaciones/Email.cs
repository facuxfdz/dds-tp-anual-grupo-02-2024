namespace AccesoAlimentario.API.Domain.Notificaciones;

public class Email : CanalNotificacion
{
    public override void Notificar(string mensaje)
    {
        Console.WriteLine($"Enviando email: {mensaje}");
    }
}