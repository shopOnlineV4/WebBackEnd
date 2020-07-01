using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.ModelView;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        // GET: api/User
        [HttpGet]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin()
        {


            return Ok(await _userManager.Users.ToListAsync());
        }



        [HttpPost]
        [Route("UserRegister")]  
        public async Task<IActionResult> UserRegister(UserMv user)
        {
            try
            {
                var data = new AppUser()
                {
                    UserName = user.UserName,
                    FirstName =  user.FirstName,
                    LastName =  user.LastName,
                    PortalCode = user.PortalCode,
                    Dob = user.Dob,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    Address = user.Address  
                };
                await _userManager.CreateAsync(data,user.Password);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
