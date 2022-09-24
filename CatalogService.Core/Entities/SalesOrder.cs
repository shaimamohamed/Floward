using CatalogService.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Core.Entities
{
    public class SalesOrder : BaseEntity
    {        
        [Required]
        public int CustomerId { get; set; }        
        [Required]
        public OrderStatusType OrderStatus { get; set; }
        
        [Required]
        public DateTime OrderCreateDate { get; set; }
        public DateTime? OrderProcessDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SalesOrderDetails> SalesOrderDetails { get; set; }
    }
}
