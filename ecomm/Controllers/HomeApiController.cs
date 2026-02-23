using ecomm.Application.Interfaces;
using ecomm.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IUserServices _user;

        public HomeApiController(IUserServices user, ICategoryServies categ, IProductServices prod)
        {
            _user = user;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User_Reg user)
        {
            _user.AddUser(user);
            return Ok("User Registered Successfully");
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login list)
        {
            var l_data = _user.Login(list);
            if (l_data.Count == 0)
                return BadRequest("Invalid Email Or Password");
            return Ok(l_data);
        }
    }
}
