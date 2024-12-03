using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Auth;

public static class LogOutUsuario
{
    public class LogOutUsuarioCommand : IRequest<IResult>
    {
    }
    
    internal class LogOutUsuarioHandler : IRequestHandler<LogOutUsuarioCommand, IResult>
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContext;

        public LogOutUsuarioHandler(ILogger<LogOutUsuarioHandler> logger,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }

        public Task<IResult> Handle(LogOutUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("LogOut usuario");

            // Remove jwt token
            _httpContext.HttpContext?.Response.Cookies.Delete("session");

            return Task.FromResult(Results.Ok());
        }
    }
}