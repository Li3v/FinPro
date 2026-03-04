using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FinPro;

public class JwtService
{
    private readonly string _secretKey;
    private readonly int _expiredMinutes;

    public JwtService(string secretKey, int expiredMinutes)
    {
        _secretKey = secretKey;
        _expiredMinutes = expiredMinutes;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var creds = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: "FinPro",
            audience: "FinProUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiredMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
