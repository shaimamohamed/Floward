using System;
using System.Collections.Generic;
using System.Text;
using CatalogService.Core.Entities;

namespace CatalogService.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {

        List<Product> GetALLProduct();
        Product GetProductById(int Id);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(int Id);
    }
}
