using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Core.Interfaces.Services;
using CatalogService.API.Models;
using CatalogService.Core.Models;

namespace CatalogService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _customerService.Login(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var username = User?.Identity?.Name;
            var response = await _customerService.GetProfile(username);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
