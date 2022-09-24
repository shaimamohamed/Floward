using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Core.Entities
{
    
    public class Catalog : BaseEntity
    {
        public DateTime? AddDate { get; set; }

        public virtual Product product { get; set; }
    }
}
