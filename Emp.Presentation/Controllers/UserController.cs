using Emp.Service.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Emp.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            Console.WriteLine("sakljdfaklj");
            return Ok(users);
        }
    }
}