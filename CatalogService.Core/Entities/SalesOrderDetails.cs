using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Core.Entities
{
    public class SalesOrderDetails : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual SalesOrder Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
