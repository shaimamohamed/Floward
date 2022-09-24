using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AssessmentProjectDbContext _db;

        public List<Product> GetCountOfProductsByStatus()
        {
            return _db.Products.Include(i => i.Category)
                                  .ToList();
        }

        public Product ChangeProductStatus(Product ProductModel)
        {
            Product product = null;
            var isSavedSuccessfully = false;
            _db.Products.Update(ProductModel);
            _db.SaveChanges();

            product = GetProductById(ProductModel.Id);

            return product;
        }

        public OrderRepository(AssessmentProjectDbContext db)
        {
            _db = db;
        }

        public SalesOrder CreateNewOrder(SalesOrder orderModel)
        {
            SalesOrder order = null;
            var isSavedSuccessfully = false;

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.SalesOrders.Add(orderModel);
                    _db.SaveChanges();

                    foreach (var d in orderModel.SalesOrderDetails)
                    {
                        var product = GetProductById(d.ProductId);
                        product.Quantity -= d.Quantity;
                        product.SoldQuantity += d.Quantity;

                        _db.Products.Update(product);
                    }
                    _db.SaveChanges();

                    transaction.Commit();
                    isSavedSuccessfully = true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

            if (isSavedSuccessfully)
            {
                order = _db.SalesOrders.Include(i => i.SalesOrderDetails).ThenInclude(it => it.Product).SingleOrDefault(a => a.Id == orderModel.Id);
            }

            return order;
        }

        public Product GetProductById(int id)
        {
            return _db.Products.SingleOrDefault(a => a.Id == id);
        }

        public SalesOrder GetOrderDetails(int orderId, int customerId)
        {
            return _db.SalesOrders.Include(i => i.SalesOrderDetails)
                                  .ThenInclude(it => it.Product)
                                  .SingleOrDefault(a => a.Id == orderId && a.CustomerId == customerId);
        }
    }
}
