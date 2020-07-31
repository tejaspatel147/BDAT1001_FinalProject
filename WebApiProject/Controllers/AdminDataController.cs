using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Services;

namespace WebApiProject.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class AdminDataController : BaseAuthenticatedController
    {
        private IUserService _userService;

        public AdminDataController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        /*[HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok(true);
        }*/
    }
}
