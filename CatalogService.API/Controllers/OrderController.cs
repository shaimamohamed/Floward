using CatalogService.Core.Interfaces.Services;
using CatalogService.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Produces("application/json")]
        [HttpGet("GetCountOfProductsByStatus")]
        public async Task<IActionResult> GetCountOfProductsByStatus()
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _orderService.GetCountOfProductsByStatus(customerId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ChangeProductStatus")]
        public async Task<IActionResult> ChangeProductStatus([FromBody] ProductRequest request)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);
            request.CustomerId = customerId;

            var response = await _orderService.ChangeProductStatus(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("CreateNewOrder")]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderRequest request)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);
            request.CustomerId = customerId;

            var response = await _orderService.CreateNewOrder(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _orderService.GetOrderDetails(orderId, customerId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
