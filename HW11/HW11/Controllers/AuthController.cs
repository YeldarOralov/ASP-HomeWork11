using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW11.DataAccess;
using HW11.DTOs;
using HW11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW11.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext context;

        public AuthController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AuthDTO auth)
        {
            var user = new User { Login = auth.Login, Password = auth.Password };
            context.Add(user);
            await context.SaveChangesAsync();
            return Ok(user.SecureCode);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AuthDTO auth)
        {
            var user = await context.Users.Where(user => user.Login == auth.Login && user.Password == auth.Password).FirstOrDefaultAsync();
            if (!(user is null))
            {
                return Ok(user.SecureCode);
            }
            else
            {
                return NotFound("User doesnt exists");
            }
        }
    }
}