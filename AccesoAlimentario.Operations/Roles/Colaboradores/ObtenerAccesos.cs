using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerAccesos
{
    public class ObtenerAccesosCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
    }

    internal class ObtenerAccesosHandler : IRequestHandler<ObtenerAccesosCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerAccesosHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerAccesosHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerAccesosCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener accesos para el colaborador {ColaboradorId}", request.ColaboradorId);
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);

            if (colaborador == null)
            {
                _logger.LogWarning("El colaborador {ColaboradorId} no existe", request.ColaboradorId);
                return Results.BadRequest("El colaborador no existe");
            }

            if (colaborador.TarjetaColaboracion == null)
            {
                _logger.LogWarning("El colaborador {ColaboradorId} no tiene una tarjeta de colaboración", request.ColaboradorId);
                return Results.BadRequest("El colaborador no tiene una tarjeta de colaboración");
            }

            var responseAccesos =
                colaborador.TarjetaColaboracion.Accesos.Select(a =>
                    _mapper.Map(a, a.GetType(), typeof(AccesoHeladeraResponse)));
            var resposeAutorizaciones = colaborador.TarjetaColaboracion.Autorizaciones.Select(a =>
                _mapper.Map(a, a.GetType(), typeof(AutorizacionManipulacionHeladeraResponse)));

            return Results.Ok(
                new
                {
                    Accesos = responseAccesos,
                    Autorizaciones = resposeAutorizaciones
                }
            );
        }
    }
}