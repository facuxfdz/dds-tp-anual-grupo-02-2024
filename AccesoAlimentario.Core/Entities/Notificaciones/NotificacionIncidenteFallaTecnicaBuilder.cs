namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class NotificacionIncidenteFallaTecnicaBuilder : INotificacionBuilder
{
    private string? _descripcion;
    private string? _foto;

    public NotificacionIncidenteFallaTecnicaBuilder(string? descripcion, string? foto)
    {
        this._descripcion = descripcion;
        this._foto = foto;
    }

    public Notificacion CrearNotificacion()
    {
        var asunto = "Acceso Alimentario: Un usuario ha reportado una Falla Técnica";
        var mensaje = "<p>Un usuario ha reportado una falla técnica en una heladera. Por favor, acudir al lugar a la brevedad.</p>";

        if (!string.IsNullOrWhiteSpace(_descripcion) || !string.IsNullOrWhiteSpace(_foto))
        {
            mensaje += "<p><strong>Información adicional proporcionada por el usuario:</strong></p>";
        }

        if (!string.IsNullOrWhiteSpace(_descripcion))
        {
            mensaje += $"<p><strong>Descripción:</strong> {_descripcion}</p>";
        }

        if (!string.IsNullOrWhiteSpace(_foto))
        {
            mensaje += $@"
                <p><strong>Foto:</strong></p>
                <img src='cid:imagenIncidente' alt='Imagen proporcionada por el usuario' style='max-width: 500px; max-height: 500px;'>";
            return new Notificacion(asunto, mensaje, [new ImagenCid { Cid = "imagenIncidente", Imagen = _foto }]);
        }
        
        return new Notificacion(asunto, mensaje);
    }
}