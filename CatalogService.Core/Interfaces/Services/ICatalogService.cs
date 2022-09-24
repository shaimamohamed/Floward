using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Core.Models;
namespace CatalogService.Core.Interfaces.Services
{
    public interface ICatalogsService
    {
        Task<GeneralResponse<List<CatalogResponse>>> GetALLCatalog();
        Task<GeneralResponse<CatalogResponse>> GetCatalogById(int catalogId);
        Task<GeneralResponse<CatalogResponse>> CreateCatalog(CatalogRequest request);
        Task<GeneralResponse<CatalogResponse>> UpdateCatalog(CatalogRequest request);
        Task<GeneralResponse<CatalogResponse>> DeleteCatalog(int catalogId);

    }
}
