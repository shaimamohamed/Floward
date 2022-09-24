using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Core.Models
{
    public class ProfileResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
