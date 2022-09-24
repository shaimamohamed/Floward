using CatalogService.Core.Enums;
using CatalogService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<GeneralResponse<List<ProductResponse>>> GetCountOfProductsByStatus(int customerId);
        Task<GeneralResponse<string>> ChangeProductStatus(ProductRequest productRequest);
        Task<GeneralResponse<OrderResponse>> CreateNewOrder(OrderRequest request);
        Task<GeneralResponse<OrderResponse>> GetOrderDetails(int orderId, int customerId);
    }
}
