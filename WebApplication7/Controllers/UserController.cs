using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Context;
using WebApplication7.Models;
using WebApplication7.Services;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;
        }
        [HttpPost, AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.TodoItems.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        [HttpPost,AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            List<User> users = _context.TodoItems.ToList();
            var user = users.Where(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();



            if (user == null)
                return NotFound(new { message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        

        [HttpGet, Authorize]
        [Route("AllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.TodoItems.ToListAsync();
        }
    }
}

