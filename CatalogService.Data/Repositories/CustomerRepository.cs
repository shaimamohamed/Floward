using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogService.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AssessmentProjectDbContext _db;

        public CustomerRepository(AssessmentProjectDbContext db)
        {
            _db = db;
        }
       
        public Customer GetCustomerByUsername(string username)
        {
            return _db.Customers.SingleOrDefault(a => a.Username.ToLower() == username.ToLower());
        }
    }
}
