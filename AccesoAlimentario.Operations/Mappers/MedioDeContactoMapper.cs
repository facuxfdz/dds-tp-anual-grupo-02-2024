using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Responses.MediosContacto;
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
        
        CreateMap<Email, EmailResponse>()
            .ForMember(x => x.Tipo, opt => opt.MapFrom(x => "Email"));

        CreateMap<MedioContacto, MedioContactoResponse>()
            .Include<Email, EmailResponse>();
    }
}