using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class SimilarProduct
    {
        public int IdSimilarProduct { get; set; }
        public int IdProduct { get; set; }
        public int IdSimilary { get; set; }

        public Products IdProductNavigation { get; set; }
    }
}
