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
    public class CatalogsService : ICatalogsService
    {
        private readonly ICatalogRepository _catalogRepository;
        public CatalogsService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<GeneralResponse<List<CatalogResponse>>> GetALLCatalog() {

            var respnose = new GeneralResponse<List<CatalogResponse>>();

            var catalog = _catalogRepository.GetALLCatalog();

            if (catalog == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            List<CatalogResponse> Temp = new List<CatalogResponse>();
            foreach (var item in catalog)
            {
                var catalogResponse = new CatalogResponse
                {
                    Code = item.product.Code,
                    Name = item.product.Name,
                    Cost = item.product.Cost,
                    Price = item.product.PurchaseSingleItemPrice,
                    ImageBase64 = item.product.ImageBase64,
                    CreateDate = item.product.CreateDate,
                    UpdateDate = item.product.UpdateDate
                };
                Temp.Add(catalogResponse);
            }


            respnose = new GeneralResponse<List<CatalogResponse>>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;

        }
        public async Task<GeneralResponse<CatalogResponse>> GetCatalogById(int catalogId) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Catalog , CatalogResponse>());

            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = catalogId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var catalog = _catalogRepository.GetCatalogById(catalogId);

            if (catalog == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            CatalogResponse Temp = new CatalogResponse();

            var mapper = new Mapper(config);
             Temp = mapper.Map<CatalogResponse>(catalog);

            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<CatalogResponse>> CreateCatalog(CatalogRequest request) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CatalogRequest, Catalog>());
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Catalog, CatalogResponse>());

            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = request != null;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Catalog catalog = new Catalog();

            var mapper = new Mapper(config);
            catalog = mapper.Map<Catalog>(request);

            var result = _catalogRepository.CreateCatalog(catalog);

            if (result == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var Temp = new CatalogResponse();

            var mapper2 = new Mapper(config2);
            Temp = mapper.Map<CatalogResponse>(result);

            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<CatalogResponse>> UpdateCatalog(CatalogRequest request) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CatalogRequest, Catalog>());
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Catalog, CatalogResponse>());

            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = request != null;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Catalog catalog = new Catalog();

            var mapper = new Mapper(config);
            catalog = mapper.Map<Catalog>(request);

            var result = _catalogRepository.UpdateCatalog(catalog);

            if (result == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var Temp = new CatalogResponse();

            var mapper2 = new Mapper(config2);
            Temp = mapper.Map<CatalogResponse>(result);

            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }
        public async Task<GeneralResponse<CatalogResponse>> DeleteCatalog(int catalogId) {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Catalog, CatalogResponse>());

            var respnose = new GeneralResponse<CatalogResponse>();

            var isValid = catalogId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            Catalog catalog = new Catalog();

            catalog = _catalogRepository.GetCatalogById(catalogId);

            if (catalog == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }
            var result = _catalogRepository.DeleteCatalog(catalogId);


            var Temp = new CatalogResponse();

            var mapper = new Mapper(config);
            Temp = mapper.Map<CatalogResponse>(result);


            respnose = new GeneralResponse<CatalogResponse>()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }

    }
}
