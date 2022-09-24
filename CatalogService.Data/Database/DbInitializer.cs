using CatalogService.Core.Entities;
using CatalogService.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogService.Data.Database
{
    public static class DbInitializer
    {
        public static void Initialize(AssessmentProjectDbContext context)
        {
            context.Database.EnsureCreated();

            //seed customers
            if (!context.Customers.Any())
            {
                string password = "password123";

                string passowrdHash1;
                string passwordSalt1;
                HashingHelper.CreatePasswordHashAndSalt(password, out passowrdHash1, out passwordSalt1);

                string passowrdHash2;
                string passwordSalt2;
                HashingHelper.CreatePasswordHashAndSalt(password, out passowrdHash2, out passwordSalt2);

                string passowrdHash3;
                string passwordSalt3;
                HashingHelper.CreatePasswordHashAndSalt(password, out passowrdHash3, out passwordSalt3);

                var customers = new Customer[]
                {
                new Customer{Username="CustomerA",
                    FirstName="Customer",
                    LastName = "A",
                    PasswordHash=passowrdHash1,
                    PasswordSalt=passwordSalt1,
                    CreateDate= DateTime.Now},
                new Customer{Username="CustomerB",
                    FirstName="Customer",
                    LastName = "B",
                    PasswordHash=passowrdHash2,
                    PasswordSalt=passwordSalt2,
                    CreateDate= DateTime.Now},
                new Customer{Username="Admin",
                    FirstName="System",
                    LastName = "Admin",
                    PasswordHash=passowrdHash3,
                    PasswordSalt=passwordSalt3,
                    CreateDate= DateTime.Now}
                };

                foreach (var c in customers)
                {
                    context.Customers.Add(c);
                }

                context.SaveChanges();
            }

            //seed products
            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product{
                        Code = "P1",
                        Name = "Product 1",
                        Quantity = 100,
                        SoldQuantity = 0,
                        DamagedQuantity = 0,
                        Cost = 18,
                        PurchaseSingleItemPrice = 30,
                        PurchaseSingleItemPriceCurrencyCode = "AED",
                        ImageBase64 = System.Guid.NewGuid().ToString(),
                        CreateDate = DateTime.Now,
                        Category = new Category {
                            Code = "C1",
                            Name = "Category 1",
                            CreateDate = DateTime.Now
                        }
                    },
                    new Product{
                        Code = "P2",
                        Name = "Product 2",
                        Quantity = 200,
                        SoldQuantity = 0,
                        DamagedQuantity = 0,
                        Cost = 35,
                        PurchaseSingleItemPrice = 50,
                        PurchaseSingleItemPriceCurrencyCode = "AED",
                        ImageBase64 = System.Guid.NewGuid().ToString(),
                        CreateDate = DateTime.Now,
                        Category = new Category {
                            Code = "C2",
                            Name = "Category 2",
                            CreateDate = DateTime.Now
                        }
                    }
                };

                foreach (var p in products)
                {
                    context.Products.Add(p);
                }

                context.SaveChanges();
            }

            ////seed Catalog
            //if (!context.Catalog.Any())
            //{
            //    var catalog = new Catalog[]
            //    {
            //        new Catalog{
            //            product = new Product {
            //            Code = "P1",
            //            Name = "Product 1",
            //            Quantity = 100,
            //            SoldQuantity = 0,
            //            DamagedQuantity = 0,
            //            Cost = 0,
            //            ImageBase64 = System.Guid.NewGuid().ToString(),
            //            CreateDate = DateTime.Now
            //            }
            //        },
            //        new Catalog{
            //            product = new Product {
            //            Code = "P2",
            //            Name = "Product 2",
            //            Quantity = 200,
            //            SoldQuantity = 0,
            //            DamagedQuantity = 0,
            //            Cost = 0,
            //            ImageBase64 = System.Guid.NewGuid().ToString(),
            //            CreateDate = DateTime.Now
            //            }
            //        }
            //    };

            //    foreach (var c in catalog)
            //    {
            //        context.Catalog.Add(c);
            //        context.SaveChanges();

            //    }

            //    //context.SaveChanges();
            //}

        }
    }
}
