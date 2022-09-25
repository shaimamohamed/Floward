using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CatalogService.Data.Database;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data.Repositories;
using CatalogService.Service.Services;
using CatalogService.Core.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CatalogService.UnitTests.Services
{
    public class OrderServiceTests
    {

        [Fact]
        public void GetProductsByStatus_ResultDataShouldNotBeNull()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);

                //Act
                var response = service.GetCountOfProductsByStatus(1).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
            }
        }

        [Fact]
        public void ChangeProductStatus_ResultDataShouldNotBeNull()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);

                var request = new ProductRequest
                {
                    CustomerId = 1,
                    ProductId = 2,
                    QTY = 5,
                    Status = Core.Enums.ProductStatusType.Damaged
                };

                //Act
                var response = service.ChangeProductStatus(request).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
            }
        }


        [Fact]
        public void CreateNewOrder_ResultDataShouldNotBeNull()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);

                var request = new OrderRequest
                {
                    CustomerId = 1,
                    OrderDetailsList = new List<OrderDetailsRequest>
                    {
                        new OrderDetailsRequest
                        {
                            ProductId = 1,
                            Quantity = 10
                        },
                        new OrderDetailsRequest
                        {
                            ProductId = 2,
                            Quantity = 20
                        }
                    }
                };

                //Act
                var response = service.CreateNewOrder(request).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
            }
        }

        [Fact]
        public void CreateNewOrder_OrderQuantityShouldBeSubtractedFromProductTotalQuantity()
        {
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);


                var firstProductOrderDetail = new OrderDetailsRequest
                {
                    ProductId = 1,
                    Quantity = 10
                };
                var secondProductOrderDetail = new OrderDetailsRequest
                {
                    ProductId = 2,
                    Quantity = 20
                };

                var request = new OrderRequest
                {
                    CustomerId = 1,
                    OrderDetailsList = new List<OrderDetailsRequest>
                    {
                        firstProductOrderDetail,
                        secondProductOrderDetail
                    }
                };

                //Act

                //quantities before order
                var firstProductOriginalQuantity = context.Products.Find(1).Quantity;
                var secondProductOriginalQuantity = context.Products.Find(2).Quantity;

                var response = service.CreateNewOrder(request).Result;

                //quantities after order
                var firstProductCurrentQuantity = context.Products.Find(1).Quantity;
                var secondProductCurrentQuantity = context.Products.Find(2).Quantity;

                //Assert
                Assert.True(response.Success);
                Assert.Equal(firstProductOrderDetail.Quantity, firstProductOriginalQuantity - firstProductCurrentQuantity);
                Assert.Equal(secondProductOrderDetail.Quantity, secondProductOriginalQuantity - secondProductCurrentQuantity);
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
