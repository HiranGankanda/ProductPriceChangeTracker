using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PPCT.RepositoryServices.JWTRepoServices
{
    public interface IJwtAuthTokenGenerator
    {
        JwtSecurityToken GenerateAccessToken_JwtSecurityToken(string email, string userName, IList<string> userRoles);
        string GenerateAccessToken_Token(string email, string userName, IList<string> userRoles);
        JwtSecurityToken Verify(string token);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        JwtSecurityToken NewAccessToken_JwtSecurityToken(List<Claim> authClaims);
        string NewAccessToken_Token(List<Claim> authClaims);
        string GenerateRefreshToken();
        string GetRefreshTokenValidityInDays();
    }
}
