using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Core.Models;
namespace CatalogService.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<GeneralResponse<List<ProductCRUDResponse>>> GetALLProucts();
        Task<GeneralResponse<ProductCRUDResponse>> GetProuctById(int prouctId);
        Task<GeneralResponse<ProductCRUDResponse>> CreateProuct(ProductCRUDRequest request);
        Task<GeneralResponse<ProductCRUDResponse>> UpdateProuct(ProductCRUDRequest request);
        Task<GeneralResponse<ProductCRUDResponse>> DeleteProuct(int prouctId);

    }
}
