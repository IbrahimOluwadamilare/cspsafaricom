using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CSBGlobal.Data;
using CSBGlobal.Helpers;
using CSBGlobal.Models;
using CSBGlobal.Models.AccountViewModels;
using CSBGlobal.Models.Products;
using CSBGlobal.Services;
using static CSBGlobal.Services.CustomerService;

namespace CSBGlobal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly AppSettings appSettings;

        public AuthController(ApplicationDbContext context, IOptions<AppSettings> settings )
        {
            _context = context;
            appSettings = settings.Value;
        }
        [HttpPost]
        public ActionResult Login([FromBody] LoginViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }


            if (user.Email == appSettings.Email && user.Password == appSettings.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://smeproductivity.com",
                    audience: "https://smeproductivity.com",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddYears(2),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new GenericResponse<string> { Data = tokenString, Message = null, Success = true });
            }
            else
            {
                return Unauthorized(new GenericResponse<string> { Data = null, Message = "Unauthorized", Success = false });
            }

        }
    }
}
