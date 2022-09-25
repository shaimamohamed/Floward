using CatalogService.Core.Models;
using CatalogService.Data.Database;
using CatalogService.Data.Repositories;
using CatalogService.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogService.UnitTests.Services
{
    public class ProuctServiceTest
    {
        [Fact]
        public void GetProductsById_ResultDataShouldNotBeNull()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);

                //Act
                var response = service.GetProuctById(1).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
            }
        }

        [Fact]
        public void CreateNewProduct_ResultDataShouldNotBeNull()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);

                var request = new ProductCRUDRequest
                {
                    //Id = productrequest.Id,
                    Code = "P678",
                    Name = "Product678",
                    Quantity = 300,
                    DamagedQuantity = 0,
                    SoldQuantity = 0,
                    Cost = 95,
                    Price = 120,
                    ImageBase64 = System.Guid.NewGuid().ToString(),
                    Status =0,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CategoryId = 1
                };

                //Act
                var response = service.CreateProuct(request);

                //Assert
                Assert.True(response.Result.Success);
                Assert.NotNull(response.Result.Data);
            }
        }

        private AssessmentProjectDbContext SetupAndGetInMemoryDbContext()
        {
            //Create In-memory database
            var options = new DbContextOptionsBuilder<AssessmentProjectDbContext>()
                .UseInMemoryDatabase(databaseName: $"CatalogServiceInMemoryDb_{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //Create Mocked Context and seed data 
            var context = new AssessmentProjectDbContext(options);
            DbInitializer.Initialize(context);

            return context;
        }

    }
}
