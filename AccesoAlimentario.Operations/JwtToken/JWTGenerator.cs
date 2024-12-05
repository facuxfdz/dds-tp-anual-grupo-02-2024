using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AccesoAlimentario.Operations.JwtToken;

public class JwtGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public JwtGenerator(int expirationMinutes)
    {
        _secretKey = "my_secret_key_super_secure_3123123!!!!FF";
        _issuer = "acceso_alimentario";
        _audience = "acceso_alimentario_front";
        _expirationMinutes = expirationMinutes > 0
            ? expirationMinutes
            : throw new ArgumentOutOfRangeException(nameof(expirationMinutes));
    }

    
}