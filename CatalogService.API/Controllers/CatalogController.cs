using CatalogService.Core.Entities;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogsService _catalogService;

        public CatalogController(ICatalogsService catalogService)
        {
            _catalogService = catalogService;
        }

        #region
        [Produces("application/json")]
        [HttpGet("GetAllCatalogProducts")]
        public async Task<IActionResult> GetAllCatalogProducts()
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _catalogService.GetALLCatalog();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetCatalogProductById")]
        public async Task<IActionResult> GetCatalogProductById(int catalogId)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);

            var response = await _catalogService.GetCatalogById(catalogId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("CreateNewCatalogProduct")]
        public async Task<GeneralResponse<CatalogResponse>> CreateNewCatalogProduct([FromBody] CatalogRequest request)
        {
            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = !string.IsNullOrEmpty(request.Code)&& !string.IsNullOrEmpty(request.Name);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }


            //var catalogModel = new Catalog
            //{
            //    Code = request.Code,
            //    Name = request.Name,
            //    Cost = request.Cost,
            //    Price = request.Price,
            //    ImageBase64 = request.ImageBase64,
            //    CreateDate = DateTime.Now,
            //    UpdateDate = DateTime.Now              
            //};

            var catalog = _catalogService.CreateCatalog(request);

            if (catalog == null)
            {
                respnose.Message = "Save Error";
                return respnose;
            }

            var catalogResponse = new CatalogResponse
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

            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = catalogResponse
            };

            return respnose;
        }

        [HttpPut("UpdateCatalogProduct")]
        public async Task<GeneralResponse<CatalogResponse>> UpdateCatalogProduct([FromBody] CatalogRequest request)
        {
            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = !string.IsNullOrEmpty(request.Code) && !string.IsNullOrEmpty(request.Name);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }


            var catalog = _catalogService.UpdateCatalog(request);

            if (catalog == null)
            {
                respnose.Message = "Save Error";
                return respnose;
            }

            var catalogResponse = new CatalogResponse
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

            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = catalogResponse
            };

            return respnose;
        }

        [HttpDelete("DeleteCatalogProduct")]
        public async Task<IActionResult> DeleteCatalogProduct(int catalogId)
        {
            var Id = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out Id);

            var response = await _catalogService.DeleteCatalog(catalogId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        #endregion
    }
}
