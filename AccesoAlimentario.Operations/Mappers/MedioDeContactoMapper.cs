using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class MedioDeContactoMapper : Profile
{
    public MedioDeContactoMapper()
    {
        CreateMap<MedioDeContactoRequest, MedioContacto>()
            .Include<TelefonoRequest, Telefono>()
            .Include<EmailRequest, Email>()
            .Include<WhatsappRequest, Whatsapp>();
        
        CreateMap<TelefonoRequest, Telefono>();
        CreateMap<EmailRequest, Email>();
        CreateMap<WhatsappRequest, Whatsapp>();
    }
}