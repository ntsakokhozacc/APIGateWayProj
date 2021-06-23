using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using org.apache.commons.codec.binary;
using ReportApi.Modals.DTO.Request;
using StudentApi.Configuration;

namespace ReportApi.Controllers
{

    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager; //UserManager<dbName>
        private readonly JwtConfig _jwtConfig;
        //private readonly string Token;
        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>(){
                       "Email already in use "
                     },
                        TokenSuccess = false
                    });
                }
                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email, PhoneNumber = user.Roles.ToUpper() };//check here to add roles
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse()
                    {
                        TokenSuccess = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        TokenSuccess = false
                    });
                }

            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>(){
                    "Invalid payload"
                },
                TokenSuccess = false
            });
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>(){
                    "Invalid login request"
                     },
                        TokenSuccess = false,

                    });

                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>(){
                    "Invalid login request"
                },
                        TokenSuccess = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse() {
                    TokenSuccess = true,
                    Token = jwtToken
                });
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>(){
                    "Invalid payload"
                },
                TokenSuccess = false
            });
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {

                    new Claim("Id" ,user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,user.PhoneNumber)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }
        [HttpGet]
        [Route("/DecodeToken")]
        public string[] DecodeJwt(string Token)
        {
           var Handler = new JwtSecurityTokenHandler();
           var Decode = Handler.ReadJwtToken(Token);
           var TokenS = Decode as JwtSecurityToken;
           string Email = TokenS.Claims.First(Claim =>Claim.Type =="email").Value;
           string Role = TokenS.Claims.First(Claim => Claim.Type =="role").Value;
           //Console.WriteLine(Email+Role);
           string [] Details = {Email,Role};
           return Details;

        }

    }
}

