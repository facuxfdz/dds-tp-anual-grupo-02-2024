using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public abstract class Colaborador
{
    private Usuario _usuario; 
    private List<FormaContribucion> _formasDeContribucion;
    private List<IMedioContacto> _mediosDeContacto;

    public abstract void Colaborar(FormaContribucion formaContribucion);

    public abstract void Contactar();
}