using System.Diagnostics.CodeAnalysis;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public abstract class Colaborador
{
    private Usuario _usuario; 
    private List<FormaContribucion> _formasDeContribucion;
    private List<IMedioContacto> _mediosDeContacto;
    private float _puntos;

    public abstract void Colaborar(FormaContribucion formaContribucion);

    public abstract void Contactar();

    public float obtenerPuntos()
    {
        return _formasDeContribucion.Sum(contribucion => contribucion.CalcularPuntos());
    }
}