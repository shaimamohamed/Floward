using AutoMapper;
using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Core.Interfaces.Services;
using CatalogService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GeneralResponse<List<ProductCRUDResponse>>> GetALLProucts() {

            var respnose = new GeneralResponse<List<ProductCRUDResponse>>();

            var products = _productRepository.GetALLProduct();

            if (products == null || products.Count <= 0)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            List<ProductCRUDResponse> Temp = new List<ProductCRUDResponse>();
            foreach (var item in products)
            {
                var productResponse = new ProductCRUDResponse
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Cost = item.Cost,
                    Price = item.PurchaseSingleItemPrice,
                    ImageBase64 = item.ImageBase64,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate
                };
                Temp.Add(productResponse);
            }


            respnose = new GeneralResponse<List<ProductCRUDResponse>>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;

        }
        public async Task<GeneralResponse<ProductCRUDResponse>> GetProuctById(int prouctId) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product , ProductCRUDResponse>());

            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = prouctId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var product = _productRepository.GetProductById(prouctId);

            if (product == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            ProductCRUDResponse Temp = new ProductCRUDResponse();

            var mapper = new Mapper(config);
             Temp = mapper.Map<ProductCRUDResponse>(product);

            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<ProductCRUDResponse>> CreateProuct(ProductCRUDRequest request) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCRUDRequest, Product>());
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductCRUDResponse>());

            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = request != null;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Product prod = new Product();

            var mapper = new Mapper(config);
            prod = mapper.Map<Product>(request);

            var result = _productRepository.CreateProduct(prod);

            if (result == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var Temp = new ProductCRUDResponse();

            var mapper2 = new Mapper(config2);
            Temp = mapper.Map<ProductCRUDResponse>(result);

            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<ProductCRUDResponse>> UpdateProuct(ProductCRUDRequest request) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCRUDRequest, Product>());
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductCRUDResponse>());

            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = request != null;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Product prodd = new Product();

            var mapper = new Mapper(config);
            prodd = mapper.Map<Product>(request);

            var result = _productRepository.UpdateProduct(prodd);

            if (result == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var Temp = new ProductCRUDResponse();

            var mapper2 = new Mapper(config2);
            Temp = mapper.Map<ProductCRUDResponse>(result);

            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<ProductCRUDResponse>> DeleteProuct(int prouctId) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductCRUDResponse>());

            var respnose = new GeneralResponse<ProductCRUDResponse>();

            var isValid = prouctId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Product prod = new Product();

            prod = _productRepository.GetProductById(prouctId);

            if (prod == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }
            var result = _productRepository.DeleteProduct(prouctId);


            var Temp = new ProductCRUDResponse();

            var mapper = new Mapper(config);
            Temp = mapper.Map<ProductCRUDResponse>(result);


            respnose = new GeneralResponse<ProductCRUDResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }

    }
}
