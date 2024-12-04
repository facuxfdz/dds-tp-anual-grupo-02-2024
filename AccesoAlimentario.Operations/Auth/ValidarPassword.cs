using AccesoAlimentario.Core.Validadores.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Auth;

public static class ValidarPassword
{
    public class ValidarPasswordCommand : IRequest<IResult>
    {
        public string Password { get; set; } = string.Empty;
    }

    public class ValidarPasswordHandler : IRequestHandler<ValidarPasswordCommand, IResult>
    {
        public Task<IResult> Handle(ValidarPasswordCommand request, CancellationToken cancellationToken)
        {
            var validador = new ValidadorContrasenias();
            return Task.FromResult(Results.Ok(validador.Validar(request.Password)));
        }
    }
}