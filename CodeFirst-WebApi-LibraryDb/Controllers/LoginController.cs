using CodeFirst_WebApi_LibraryDb.Entities;
using CodeFirst_WebApi_LibraryDb.Security;
using CodeFirst_WebApi_LibraryDb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeFirst_WebApi_LibraryDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LibraryDbContext _db;
        private IConfiguration _configuration;
        private readonly IUserService _userService;

        public LoginController(LibraryDbContext db, IConfiguration configuration, IUserService userService)
        {
            _db = db;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult AuthLogin(User user)
        {
            bool isUser = ControlUser(user.UserName, user.Password);

            if (isUser)
            {
                Token token = Security.TokenHandler.CreateToken(user, _configuration);
                return Ok(token);
            }
            else { return Unauthorized(); }
        }

        private bool ControlUser(string userName, string password)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);

            if (user != null)
                return true;
            else
                return false;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetMe()
        {
            //Tokendaki username e ulaşma
            //var username = User?.Identity?.Name;
            //var username = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);
            //return Ok(new { username, role });
            
            var username = _userService.GetMyName();
            return Ok(username);
        }
    }
}
