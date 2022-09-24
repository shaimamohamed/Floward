using System;
using System.Collections.Generic;
using System.Text;
using CatalogService.Core.Entities;

namespace CatalogService.Core.Interfaces.Repositories
{
    public interface ICatalogRepository
    {

        List<Catalog> GetALLCatalog();
        Catalog GetCatalogById(int Id);
        Catalog CreateCatalog(Catalog catalog);
        Catalog UpdateCatalog(Catalog catalog);
        Catalog DeleteCatalog(int Id);
    }
}
