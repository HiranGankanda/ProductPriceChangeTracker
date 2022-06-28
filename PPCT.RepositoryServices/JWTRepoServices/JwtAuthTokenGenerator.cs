using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PPCT.RepositoryServices.JWTRepoServices
{
    public class JwtAuthTokenGenerator : IJwtAuthTokenGenerator
    {
        #region Constructor
        //MORE,
        //https://www.youtube.com/watch?v=FSUa8Vd-td0&t=313s&ab_channel=Geek%27sLesson
        //https://github.com/alimozdemir/medium/tree/master/AuthJWTRefresh
        //https://www.c-sharpcorner.com/article/jwt-authentication-with-refresh-tokens-in-net-6-0/

        private readonly IConfiguration _configuration;
        public JwtAuthTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region InterfaceCalls
        public JwtSecurityToken GenerateAccessToken_JwtSecurityToken(string email, string userName, string fullName, IList<string> userRoles)
        {
            return GenarateAccessTokenJwtSecurityToken(email, userName, fullName, userRoles);
        }
        public string GenerateAccessToken_Token(string email, string userName, string fullName, IList<string> userRoles)
        {
            return new JwtSecurityTokenHandler().WriteToken(GenarateAccessTokenJwtSecurityToken(email, userName, fullName, userRoles));
        }
        
        public JwtSecurityToken Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        public JwtSecurityToken NewAccessToken_JwtSecurityToken(List<Claim> authClaims)
        {
            return GenarateNewAccessToken(authClaims);
        }
        public string NewAccessToken_Token(List<Claim> authClaims)
        {
            return new JwtSecurityTokenHandler().WriteToken(GenarateNewAccessToken(authClaims));
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public string GetRefreshTokenValidityInDays()
        {
            return _configuration["JWT:RefreshTokenValidityInDays"];
        }
        #endregion

        #region Supporting Methods
        private JwtSecurityToken GenarateAccessTokenJwtSecurityToken(string email, string userName, string fullName, IList<string> userRoles)
        {
            try
            {
                var AuthClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.GivenName, fullName)
                };

                foreach (string userRole in userRoles)
                {
                    AuthClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
                _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: AuthClaims,
                    expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                    //expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCredentials
                );

                return token;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        private JwtSecurityToken GenarateNewAccessToken(List<Claim> authClaims)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                return new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}