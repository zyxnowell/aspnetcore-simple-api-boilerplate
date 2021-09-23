using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Core.Interfaces;
using Simple.Core.Models;
using System.Threading.Tasks;

namespace Simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _service;
        public UsersController(IIdentityService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel input)
        {
            var result = await _service.Authenticate(input);

            if (result == null)
                return BadRequest();

            return Ok(result);
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
