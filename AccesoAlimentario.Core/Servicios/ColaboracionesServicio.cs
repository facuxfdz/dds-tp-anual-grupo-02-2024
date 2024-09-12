using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Settings;

namespace AccesoAlimentario.Core.Servicios;

//TODO Los validadores de la forma de contribucion no deberian estar en la forma en si, ya que hay que primero crear el objeto al pedo y despues 
public class ColaboracionesServicio(UnitOfWork unitOfWork, ColaboradoresServicio colaboradoresServicio)
{
    //Cuando la colaboracion viene por el importador, se crea con una fecha
    public FormaContribucion CrearAdministracionHeladera(Colaborador colab, Heladera heladera, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        var formaAdministracionHeladera = new AdministracionHeladera(fecha, heladera);
        if (!VerificarColab(formaAdministracionHeladera, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.AdministracionHeladeraRepository.Insert(formaAdministracionHeladera);

        //Se le asignan los puntos al colaborador
        //Nada que agregar, ya que no se le asignan puntos por esta colaboracion


        return formaAdministracionHeladera;
    }

    public FormaContribucion CrearDistribucionViandas(Colaborador colab, Heladera heladeraOrigen,
        Heladera heladeraDestino, int cantViandas, MotivoDistribucion motivo, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;

        var formaDistribucionViandas =
            new DistribucionViandas(fecha, heladeraOrigen, heladeraDestino, cantViandas, motivo);

        if (!VerificarColab(formaDistribucionViandas, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.DistribucionViandasRepository.Insert(formaDistribucionViandas);

        //Se le asignan los puntos al colaborador
        colaboradoresServicio.AgregarPuntos(colab, AppSettings.Instance.ViandasDistribuidasCoef * cantViandas);

        return formaDistribucionViandas;
    }

    public FormaContribucion CrearRegistroPersonaVulnerable(Colaborador colab, Persona persona, int cantMenores,
        TarjetaConsumo tarjetaConsumo, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        var personaVulnerable =
                new PersonaVulnerable(persona, cantMenores, tarjetaConsumo);
        tarjetaConsumo.AsignarPropietario(personaVulnerable);
        var contribucionRegistroPersonaVul = new RegistroPersonaVulnerable(fecha, tarjetaConsumo);

        if (!VerificarColab(contribucionRegistroPersonaVul, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.RegistroPersonaVulnerableRepository.Insert(contribucionRegistroPersonaVul);
        
        //Se le asignan los puntos al colaborador
        colaboradoresServicio.AgregarPuntos(colab, AppSettings.Instance.TarjetasRepartidasCoef);

        return contribucionRegistroPersonaVul;
    }

    public FormaContribucion CrearDonacionMonetaria(Colaborador colab, float monto, int frecDias, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        var formaDonacionMonetaria = new DonacionMonetaria(fecha, monto, frecDias);

        if (!VerificarColab(formaDonacionMonetaria, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.DonacionMonetariaRepository.Insert(formaDonacionMonetaria);

        //Se le asignan los puntos al colaborador
        colaboradoresServicio.AgregarPuntos(colab, AppSettings.Instance.PesoDonadosCoef * monto);


        return formaDonacionMonetaria;
    }

    public FormaContribucion CrearDonacionVianda(Colaborador colab, Heladera heladera, Vianda vianda,
        DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        var formaDonacionVianda = new DonacionVianda(fecha, heladera, vianda);

        if (!VerificarColab(formaDonacionVianda, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.DonacionViandaRepository.Insert(formaDonacionVianda);

        //Se le asignan los puntos al colaborador
        colaboradoresServicio.AgregarPuntos(colab, AppSettings.Instance.ViandasDonadasCoef);

        return formaDonacionVianda;
    }

    public FormaContribucion CrearOfertaPremio(Colaborador colab, Premio premio, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        var formaOfertaPremio = new OfertaPremio(fecha, premio);

        if (!VerificarColab(formaOfertaPremio, fechaContr, colab))
        {
            throw new Exception("No tiene autorizacion para realizar esta colaboracion");
        }

        unitOfWork.OfertaPremioRepository.Insert(formaOfertaPremio);

        //No se asignan puntos por esta colaboracion

        return formaOfertaPremio;
    }

    public bool VerificarColab(FormaContribucion formaContri, DateTime? fecha, Colaborador colab)
    {
        //return fecha != null && formaContri.EsValido(colab);
        return true;
    }
}