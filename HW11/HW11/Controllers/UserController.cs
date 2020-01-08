using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW11.DataAccess;
using HW11.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW11.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(HttpContext context)
        {
            try
            {
                string accesToken = context.Request.Headers["auth-token"].ToString();
                User user = await _context.Users.FindAsync(accesToken);
                if (user.SecureCode == accesToken)
                {
                    return Ok($"Hello "+user.Login);
                }
                else
                {
                    return Conflict("Not authorized");
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}