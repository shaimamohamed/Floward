using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CatalogService.Data.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AssessmentProjectDbContext _db;

        public CatalogRepository(AssessmentProjectDbContext db)
        {
            _db = db;

        }

        public List<Catalog> GetALLCatalog() {

            return _db.Catalog.ToList() ?? new List<Catalog>();

        }
        public Catalog GetCatalogById(int Id) {

            return _db.Catalog.SingleOrDefault(a => a.Id == Id) ?? new Catalog(); 

        }
        public Catalog CreateCatalog(Catalog catalogModel) {

            Catalog catalog = new Catalog();
            var isSavedSuccessfully = false;

            _db.Catalog.Add(catalogModel);
            _db.SaveChanges();

            catalog = _db.Catalog.SingleOrDefault(a => a.Id == catalogModel.Id);
           
            return catalog;
        }
        public Catalog UpdateCatalog(Catalog catalogModel) {

            Catalog catalog = new Catalog();
            var isSavedSuccessfully = false;
            _db.Catalog.Update(catalogModel);
            _db.SaveChanges();

            catalog = GetCatalogById(catalogModel.Id);

            return catalog;
        }
        public Catalog DeleteCatalog(int Id)
        {
            Catalog catalog = new Catalog();
            catalog = GetCatalogById(Id);
            var isSavedSuccessfully = false;
            _db.Catalog.Remove(catalog);
            _db.SaveChanges();


            return catalog;
        }

  
    }
}
