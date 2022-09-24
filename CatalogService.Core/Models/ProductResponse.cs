using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Core.Models
{
    public class ProductResponse
    {
        public ProductResponse()
        {

        }

        public string Code { get; set; }
        public String Category { get; set; }
        public string Name { get; set; }
        public ProductStatusType Status { get; set; }
        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }
        public int DamagedQuantity { get; set; }

    }
}
