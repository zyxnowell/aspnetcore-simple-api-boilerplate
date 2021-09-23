using Microsoft.AspNetCore.Mvc;
using Simple.Core.Interfaces;
using Simple.Core.Models;
using System.Threading.Tasks;

namespace Simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _service;
        public UsersController(IIdentityService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] SetUserModel model)
        {
            var addUser = await _service.AddUser(model);

            if (addUser.Result)
                return Ok(addUser);

            return BadRequest(addUser);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _service.GetAllRoles());
        }
    }
}
