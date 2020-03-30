using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
