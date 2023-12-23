using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImageBLL.Models;

namespace ImageBLL.Helpers;

public class TokenHelper
{
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;

    public TokenHelper(string jwtKey, string jwtIssuer)
    {
        _jwtKey = jwtKey;
        _jwtIssuer = jwtIssuer;
    }

    public string GenerateJWTBearerToken(TokenConfig tokenConfig)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsList = tokenConfig.Claims.Select(claim => new Claim(claim.Key, claim.Value)).ToArray();

        var token = new JwtSecurityToken(_jwtIssuer,
            _jwtIssuer,
            claimsList,
            expires: tokenConfig.ExpirationTime.ToUniversalTime(),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}