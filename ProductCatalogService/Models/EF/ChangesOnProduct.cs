using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class ChangesOnProduct
    {
        public int IdLog { get; set; }
        public int IdProduct { get; set; }
        public int ActionMade { get; set; }
    }
}
