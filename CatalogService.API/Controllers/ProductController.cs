using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Services;
using CatalogService.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMQService _rabbitMQService;

        public ProductController(IProductService productService, IRabbitMQService rabbitMQService)
        {
            _productService = productService;
            _rabbitMQService = rabbitMQService;
        }

        #region
        [Produces("application/json")]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _productService.GetALLProucts();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _productService.GetProuctById(productId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("CreateNewProduct")]
        public async Task<GeneralResponse<ProductCRUDResponse>> CreateNewProduct([FromBody] ProductCRUDRequest request)
        {
            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = !string.IsNullOrEmpty(request.Code)&& !string.IsNullOrEmpty(request.Name);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }



            //var product = await _productService.CreateProuct(request);

            //if (product == null)
            //{
            //    respnose.Message = "Save Error";
            //    return respnose;
            //}

            //var queueMessage = JsonConvert.SerializeObject(new { product.Data.Id, product.Data.Name });
            var queueMessage = JsonConvert.SerializeObject(new { request.Id, request.Name });

            _rabbitMQService.SendToQueue(queueMessage);

            var productResponse = new ProductCRUDResponse
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Cost = request.Cost,
                Price = request.Price,
                ImageBase64 = request.ImageBase64,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = productResponse
            };

            return respnose;
        }

        [HttpPut("UpdateProduct")]
        public async Task<GeneralResponse<ProductCRUDResponse>> UpdateProduct([FromBody] ProductCRUDRequest request)
        {
            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = !string.IsNullOrEmpty(request.Code) && !string.IsNullOrEmpty(request.Name);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }


            var product = await _productService.UpdateProuct(request);

            if (product == null)
            {
                respnose.Message = "Save Error";
                return respnose;
            }

            var productResponse = new ProductCRUDResponse
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Cost = request.Cost,
                Price = request.Price,
                ImageBase64 = request.ImageBase64,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = productResponse
            };

            return respnose;
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var Id = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out Id);

            var response = await _productService.DeleteProuct(productId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        #endregion
    }
}
