using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Core.Models
{
    public class ProductRequest
    {
        public ProductRequest()
        {

        }
        public int CustomerId { get; set; }
        public int QTY { get; set; }
        public int ProductId { get; set; }
        public ProductStatusType Status { get; set; }

    }
}
