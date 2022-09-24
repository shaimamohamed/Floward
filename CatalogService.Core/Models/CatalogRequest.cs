using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Core.Models
{
    public class CatalogRequest
    {
        public CatalogRequest()
        {

        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public string ImageBase64 { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
