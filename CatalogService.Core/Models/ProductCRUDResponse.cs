using CatalogService.Core.Entities;
using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Core.Models
{
    public class ProductCRUDResponse : BaseEntity
    {
        public ProductCRUDResponse()
        {

        }
        //public int Id { get; set; }
        //public string Code { get; set; }
        //public string Name { get; set; }
        //public decimal? Cost { get; set; }
        //public decimal? Price { get; set; }
        //public string ImageBase64 { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime? UpdateDate { get; set; }

        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }
        public int DamagedQuantity { get; set; }
        public ProductStatusType Status { get; set; }
        public decimal? Price { get; set; }
        //public decimal? PurchaseSingleItemPrice { get; set; }
        public string PurchaseSingleItemPriceCurrencyCode { get; set; }
        public int? CategoryId { get; set; }

        public double? DimensionLength { get; set; }
        public double? DimensionWidth { get; set; }
        public double? DimensionHeight { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public decimal? Cost { get; set; }
        public string ImageBase64 { get; set; }

        //public Category Category { get; set; }

    }
}
