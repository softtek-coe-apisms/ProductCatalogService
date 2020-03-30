using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class CatTypeProduct
    {
        public int IdType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
