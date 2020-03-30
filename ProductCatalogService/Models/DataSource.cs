using ProductCatalogService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogService.Models
{
    public class DataSource
    {
        public const string USD = "USD";
        protected DataProductsContext db;
    }
}
