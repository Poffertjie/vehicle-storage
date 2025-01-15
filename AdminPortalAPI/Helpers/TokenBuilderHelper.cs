using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace AdminPortalAPI.Helpers;

public class TokenBuilderHelper
{
    public static string BuildToken(string userId, bool systemAdmin, int? companyId, List<string> roles)
    {
        SigningCredentials? signingCredentials = GetSigningCredentials();
        JwtSecurityToken? tokenOptions =
            GenerateTokenOptions(signingCredentials, SetClaims(userId, systemAdmin, companyId, roles));
        string? token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return token;
    }

    private static List<Claim> SetClaims(string userId, bool systemAdmin, int? companyId, List<string> roles)
    {
        List<Claim>? claims = new List<Claim>()
        {
            new(ClaimTypes.UserData, userId),
        };
        
        if(companyId.HasValue)
            claims.Add(new Claim(StringConstants.CompanyId, companyId.Value.ToString()));

        if (systemAdmin)
            claims.Add(new Claim(ClaimTypes.Role, StringConstants.SystemAdmin));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, CapitalizeFirstLetter(role)));
        }
         
        return claims;
    }

    private static SigningCredentials GetSigningCredentials()
    {
        byte[]? key = Encoding.UTF8.GetBytes(Config.JwtKey);
        SymmetricSecurityKey? secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private static JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        JwtSecurityToken? tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
    
    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        input = input.ToLower(CultureInfo.InvariantCulture); // Convert the string to lowercase
        return char.ToUpper(input[0], CultureInfo.InvariantCulture) + input.Substring(1);
    }
}