using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class ColaboradoresServicio(UnitOfWork unitOfWork, PersonasServicio personasServicio)
{
    public Colaborador Crear(Persona persona, List<TipoContribucion> tipoContribuciones)
    {
        //TODO validar que la persona este cargada en la base de datos
        var colab = new Colaborador(persona, tipoContribuciones);
        unitOfWork.ColaboradorRepository.Insert(colab);
        personasServicio.AgregarRol(persona.Id, colab);

        return colab;
    }

    public void Eliminar(int id)
    {
        var colab = unitOfWork.ColaboradorRepository.GetById(id);
        if (colab != null)
            unitOfWork.ColaboradorRepository.Delete(colab);
        else
        {
            throw new Exception("No se encontro el colaborador");
        }
    }

    public void Modificar(Colaborador colaborador, List<TipoContribucion> contriPrefe)
    {
        //TODO falta la parte de persistir los cambios en la base de datos
        /*colaborador.ContribucionesPreferidas = contriPrefe;*/
    }

    public ICollection<Colaborador> ObtenerTodos()
    {
        return unitOfWork.ColaboradorRepository.Get().ToList();
    }

    public Colaborador Buscar(int id)
    {
        return unitOfWork.ColaboradorRepository.GetById(id);
    }

    public void AsignarTarjeta(int idColab, Tarjeta tarjeta)
    {
        var colab = unitOfWork.ColaboradorRepository.GetById(idColab);
        colab.AsignarTarjeta(tarjeta as TarjetaColaboracion);
        unitOfWork.ColaboradorRepository.Update(colab);
    }

    public float ObtenerPuntos(Colaborador colaborador)
    {
        return colaborador.Puntos;
    }

    public void AgregarPuntos(Colaborador colaborador, float puntos)
    {
        colaborador.AgregarPuntos(puntos);
        unitOfWork.ColaboradorRepository.Update(colaborador);
    }

    public void DescontarPuntos(Colaborador colaborador, float puntos)
    {
        colaborador.DescontarPuntos(puntos);
        unitOfWork.ColaboradorRepository.Update(colaborador);
    }

    public void AgregarSuscripcion(Colaborador colaborador, Heladera heladera, Suscripcion suscripcion)
    {
        //TODO
        throw new NotImplementedException();
    }

    public void EliminarSuscripcion(Colaborador colaborador, Heladera heladera, Suscripcion suscripcion)
    {
        //TODO
        throw new NotImplementedException();
    }

    public void DesuscribirseAHeladera(Suscripcion suscripcion)
    {
        //TODO
        throw new NotImplementedException();
    }
}