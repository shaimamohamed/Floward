using System;
using System.Collections.Generic;
using System.Text;
using CatalogService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CatalogService.Data.Database
{
    public partial class AssessmentProjectDbContext : DbContext
    {
        public AssessmentProjectDbContext()
        {
        }

        public AssessmentProjectDbContext(DbContextOptions<AssessmentProjectDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=DESKTOP-C6157GT; Database=CatalogServiceDb; Trusted_Conn‌ection=True; Multiple‌​ActiveResultSets=tru‌​e;");
                optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER14;Database=AssessmentProjectDb;Trusted_Conn‌ection=True;Multiple‌​ActiveResultSets=tru‌​e;");
                //optionsBuilder.UseSqlServer("Data Source=ALMOGDAD-PC\\MSSQLSERVER14;Database=AssessmentProjectDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasIndex(b => b.Username)
                        .IsUnique();

            modelBuilder.Entity<Category>()
                        .HasIndex(b => b.Code)
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .HasIndex(b => b.Code)
                        .IsUnique();

            modelBuilder.Entity<SalesOrderDetails>()
                        .HasIndex(b => new { b.OrderId, b.ProductId })
                        .IsUnique();

            modelBuilder.Entity<Catalog>()
                         .HasIndex(b => b.Id)
                         .IsUnique();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
