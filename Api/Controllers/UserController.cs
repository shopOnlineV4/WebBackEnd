using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Common;
using Api.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelViews;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationSetting _appSetting;
        public UserController(UserManager<AppUser> userManager,
             RoleManager<AppRole> roleManager,
             SignInManager<AppUser> signInManager,
             IOptions<ApplicationSetting> appSetting
             )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSetting = appSetting.Value;
        }
        // GET: api/<UserController>
        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister(UserMv user)
        {
            try
            {
                var data = new AppUser()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PortalCode = user.PortalCode,
                    Dob = user.Dob,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    Address = user.Address
                };
                await _userManager.CreateAsync(data, user.Password);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        [HttpPost]
        [Route("Login")]
        // POST: api/<UserController>/Login
        public async Task<IActionResult> Login(LogingModel model)
        {
            //login
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                //create a tokenDescriptor with userId to generate token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var security = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(security);
                return Ok(new { token });
            }
            else return BadRequest(new { message = "userName or Password is correct" });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName
            };
        }
    }
}