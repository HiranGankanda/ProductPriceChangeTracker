using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PPCT.DataSupport;
using PPCT.DataSupport.CustomModels;
using PPCT.RepositoryServices.JWTRepoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IJwtAuthTokenGenerator jwtAuthTokenGenerator;
        public AuthenticateController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            IJwtAuthTokenGenerator jwtAuthTokenGenerator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtAuthTokenGenerator = jwtAuthTokenGenerator;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<string> userRoles = await userManager.GetRolesAsync(user);
                
                var token = jwtAuthTokenGenerator.GenerateAccessToken_Token(user.Email, user.UserName, user.FirstName+" " + user.LastName, userRoles);
                var refreshToken = jwtAuthTokenGenerator.GenerateRefreshToken();                

                _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await userManager.UpdateAsync(user);

                return Ok(new TokenModel
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    //Expiration = token.ValidTo,
                    Message = "Success"
                });
            }
            return Unauthorized(new TokenModel
            {
                AccessToken = null,
                RefreshToken = null,
                Expiration = null,
                Message = "Invalid User Login"
            });
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Success"
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response
                { 
                    StatusID = 409,
                    Status = "Status409Conflict", 
                    Message = "Username already exists!" 
                });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { 
                    StatusID = 500,
                    Status = "Status500InternalServerError", 
                    Message = "User creation failed! Please check user details and try again." 
                });

            return Ok(new Response
            { 
                StatusID = 0,
                Status = "Success", 
                Message = "User created successfully!"
            });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response 
                { 
                    Status = "Error", 
                    Message = "User already exists!" 
                });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response 
                { 
                    Status = "Error", 
                    Message = "User creation failed! Please check user details and try again." 
                });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;
            
            var principal = jwtAuthTokenGenerator.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;
            var user = await userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = jwtAuthTokenGenerator.NewAccessToken_Token(principal.Claims.ToList());
            var newRefreshToken = jwtAuthTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);

            return new ObjectResult(new TokenModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = null,
                Message = "Success"
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("livecall")]
        public IActionResult LiveTokenCall()
        {
            //return await Task.Run(() => Ok(new TokenModel { 
            //    AccessToken = null, 
            //    Expiration = null, 
            //    RefreshToken = null, 
            //    Message = "Success" 
            //}));

            return Ok(new TokenModel
            {
                AccessToken = null,
                Expiration = null,
                RefreshToken = null,
                Message = "Success"
            });
        }
    }
}