using CatalogService.Core.Entities;
using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        List<Product> GetCountOfProductsByStatus();
        Product ChangeProductStatus(Product ProductModel);
        SalesOrder CreateNewOrder(SalesOrder orderModel);
        SalesOrder GetOrderDetails(int orderId,int customerId);
        Product GetProductById(int id);
    }
}
