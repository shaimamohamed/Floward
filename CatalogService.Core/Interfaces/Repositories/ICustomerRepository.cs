using CatalogService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByUsername(string username);
    }
}
