using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;
using AccesoAlimentario.Operations.Roles.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ImportarColaboradoresCsv
{
    public class ImportarColaboradoresCsvCommand : IRequest<IResult>
    {
        public string Archivo { get; set; } = string.Empty;
    }

    public class ImportarColaboradoresCsvHandler : IRequestHandler<ImportarColaboradoresCsvCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ImportarColaboradoresCsvHandler(IUnitOfWork unitOfWork, IMediator mediator,
            ILogger<ImportarColaboradoresCsvHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IResult> Handle(ImportarColaboradoresCsvCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Importando colaboradores");
            using var streamFile = new MemoryStream(Convert.FromBase64String(request.Archivo));
            var importador = new ImportadorCsv();
            var colaboradores = importador.ImportarColaboradores(streamFile);
            _logger.LogInformation($"Se importaran {colaboradores.Count} colaboradores");

            foreach (var colaborador in colaboradores)
            {
                var query = _unitOfWork.PersonaHumanaRepository.GetQueryable();
                query = query
                        .Where(x => x.DocumentoIdentidad!.TipoDocumento == colaborador.Persona.DocumentoIdentidad!.TipoDocumento)
                        .Where(x => x.DocumentoIdentidad!.NroDocumento == colaborador.Persona.DocumentoIdentidad!.NroDocumento)
                    ;
                var p = await _unitOfWork.PersonaHumanaRepository.GetAsync(query);
                if (p == null)
                {
                    _logger.LogInformation($"Creando colaborador {colaborador.Persona.DocumentoIdentidad!.TipoDocumento} {colaborador.Persona.DocumentoIdentidad!.NroDocumento}");
                    var personaHumana = (PersonaHumana)colaborador.Persona;
                    var usuarioSistema = (UsuarioSistema)colaborador.Persona.Roles.Find(x => x is UsuarioSistema)!;
                    personaHumana.Roles.Remove(usuarioSistema);
                    await _unitOfWork.PersonaHumanaRepository.AddAsync(personaHumana);
                    await _unitOfWork.SaveChangesAsync();
                    await _mediator.Send(new CrearUsuario.CrearUsuarioCommand
                    {
                        PersonaId = personaHumana.Id,
                        Username = usuarioSistema.UserName,
                        Password = usuarioSistema.Password
                    }, cancellationToken);
                }
                else
                {
                    _logger.LogInformation($"Actualizando colaborador {colaborador.Persona.DocumentoIdentidad!.TipoDocumento} {colaborador.Persona.DocumentoIdentidad!.NroDocumento}");
                    var poseeUsuario = p.Roles.Any(x => x is UsuarioSistema);
                    if (!poseeUsuario)
                    {
                        var usuarioSistema = (UsuarioSistema)colaborador.Persona.Roles.Find(x => x is UsuarioSistema)!;
                        await _mediator.Send(new CrearUsuario.CrearUsuarioCommand
                        {
                            PersonaId = p.Id,
                            Username = usuarioSistema.UserName,
                            Password = usuarioSistema.Password
                        }, cancellationToken);
                    }

                    var esColaborador = p.Roles.Any(x => x is Colaborador);
                    if (!esColaborador)
                    {
                        p.Roles.Add(colaborador);
                        await _unitOfWork.RolRepository.AddAsync(colaborador);
                    }
                    else
                    {
                        var c = (Colaborador)p.Roles.Find(x => x is Colaborador)!;
                        colaborador.ContribucionesRealizadas.ForEach(c.AgregarContribucion);
                    }

                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return Results.Ok();
        }
    }
}