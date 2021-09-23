using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SampleController : ControllerBase
    {

        private readonly ISampleService _service;
        public SampleController(ISampleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        
        [AllowAnonymous]
        [HttpGet("sample-anonymous")]
        public async Task<IActionResult> GetSampleAnonymous()
        {
            return Ok("Hi, I am a sample public endpoint!");
        }
    }
}
