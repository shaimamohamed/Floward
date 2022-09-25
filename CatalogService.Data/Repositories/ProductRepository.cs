using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CatalogService.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AssessmentProjectDbContext _db;

        public ProductRepository(AssessmentProjectDbContext db)
        {
            _db = db;

        }

        public List<Product> GetALLProduct() {

            return _db.Products.ToList() ?? new List<Product>();

        }
        public Product GetProductById(int Id) {

            return _db.Products.SingleOrDefault(a => a.Id == Id) ?? new Product(); 

        }
        public Product CreateProduct(Product product) {

            Product productitem = new Product();
            var isSavedSuccessfully = false;

            _db.Products.Add(product);
            _db.SaveChanges();

            //productitem = _db.Products.SingleOrDefault(a => a.Id == product.Id);
            productitem = _db.Products.SingleOrDefault(a => a.Name == product.Name && a.Code == product.Code);

            return productitem;
        }
        public Product UpdateProduct(Product product) {

            Product productitem = new Product();
            var isSavedSuccessfully = false;
            _db.Products.Update(product);
            _db.SaveChanges();

            productitem = GetProductById(product.Id);

            return productitem;
        }
        public Product DeleteProduct(int Id)
        {
            Product product = new Product();
            product = GetProductById(Id);
            var isSavedSuccessfully = false;
            _db.Products.Remove(product);
            _db.SaveChanges();


            return product;
        }

  
    }
}
