using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;
using AccesoAlimentario.Operations.Roles.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ImportarColaboradoresCsv
{
    public class ImportarColaboradoresCsvCommand : IRequest<IResult>
    {
        public string Archivo { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<ImportarColaboradoresCsvCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public Handler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<IResult> Handle(ImportarColaboradoresCsvCommand request, CancellationToken cancellationToken)
        {
            using var streamFile = new MemoryStream(Convert.FromBase64String(request.Archivo));
            var importador = new ImportadorCsv();
            var colaboradores = importador.ImportarColaboradores(streamFile);
            
            foreach (var colaborador in colaboradores)
            {
                var query = _unitOfWork.PersonaHumanaRepository.GetQueryable();
                query = query.Where(x => x.DocumentoIdentidad == colaborador.Persona.DocumentoIdentidad);
                var p = await _unitOfWork.PersonaHumanaRepository.GetAsync(query);
                if (p == null)
                {
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