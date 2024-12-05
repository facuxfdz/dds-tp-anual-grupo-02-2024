using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using Microsoft.IdentityModel.Tokens;

namespace AccesoAlimentario.Core.Tokens;

public static class TokenUsuario
{
    private const string SecretKey = "my_secret_key_super_secure_3123123!!!!FF";
    private const string Issuer = "acceso_alimentario";
    private const string Audience = "acceso_alimentario_front";
    private const int ExpirationMinutes = 60;

    public static string GenerarToken(UsuarioSistema usuario)
    {
        var persona = usuario.Persona;
        var rolColaborador = persona.Roles.OfType<Colaborador>().FirstOrDefault();
        var tarjetaColaboradorId = "";
        if (rolColaborador != null)
        {
            tarjetaColaboradorId = rolColaborador.TarjetaColaboracion?.Id.ToString() ?? "";
        }

        var rolTecnico = persona.Roles.OfType<Tecnico>().FirstOrDefault();
        var contribucionesPreferidasInt =
            rolColaborador?.ContribucionesPreferidas.Select(c => (int)c).ToArray() ?? [];
        var newToken = Generar(usuario.Id.ToString(),
        [
            new KeyValuePair<string, string>("usuarioId", usuario.Id.ToString() ?? ""),
            new KeyValuePair<string, string>("colaboradorId", rolColaborador?.Id.ToString() ?? ""),
            new KeyValuePair<string, string>("tecnicoId", rolTecnico?.Id.ToString() ?? ""),
            new KeyValuePair<string, string>("name", persona.Nombre),
            new KeyValuePair<string, string>("profile_picture", usuario.ProfilePicture ?? ""),
            new KeyValuePair<string, string>("contribucionesPreferidas",
                string.Join(",", contribucionesPreferidasInt)),
            new KeyValuePair<string, string>("tarjetaColaboracionId", tarjetaColaboradorId),
            new KeyValuePair<string, string>("personaTipo",
                persona switch
                {
                    PersonaHumana => "Humana",
                    PersonaJuridica => "Juridica",
                    _ => ""
                })
        ]);
        return newToken;
    }

    private static string Generar(string userId, List<KeyValuePair<string, string>> additionalClaims = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(SecretKey);

        // Configura los claims básicos.
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Identificador único del token.
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64), // Fecha de emisión.
        };

        // Agrega cualquier claim adicional.
        if (additionalClaims != null)
        {
            foreach (var claim in additionalClaims)
            {
                claims.Add(new Claim(claim.Key, claim.Value));
            }
        }

        // Configura la clave de seguridad.
        var signingKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // Configura los detalles del token.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(ExpirationMinutes),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = signingCredentials
        };

        // Genera el token.
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static Guid ObtenerId(string token)
    {
        try
        {
            // Configurar el manejador del token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            // Parámetros de validación
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = false, // Verifica que no esté expirado
                ClockSkew = TimeSpan.Zero // No permite margen de tiempo
            };

            // Intentar validar y leer el token
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            // Extraer el claim del userId
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "usuarioId");

            if (userIdClaim == null)
                throw new SecurityTokenException("El token no contiene un userId.");

            // Convertir el userId a GUID
            return Guid.Parse(userIdClaim.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al obtener el id del token");
            Console.WriteLine(e);
            return Guid.Empty;
        }
    }
}