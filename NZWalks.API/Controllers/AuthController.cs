using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterReqDto registerReqDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerReqDto.Username,
                Email = registerReqDto.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerReqDto.Password);
            if (identityResult.Succeeded)
            {
                //add roles to this user
                if(registerReqDto.Roles != null && registerReqDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRoleAsync(identityUser, registerReqDto.Roles[0]);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User registerd plz login");
                    }
                }

            }
            return BadRequest("Something went wrong");
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginReqDto loginReqDto)
        {
            var user = await userManager.FindByEmailAsync(loginReqDto.Username); 
            
            if (user != null) {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user,loginReqDto.Password);
                if (checkPasswordResult) {
                    //get roles for user
                    var roles =await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user,roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }



                    return Ok();
                }
            }

            return BadRequest("Username or password incorrect");

        }
    }
}
