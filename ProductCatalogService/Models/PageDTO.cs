﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogService.Models
{
    public class PageDTO
    {

        public List<ProductDTO> Products { get; set; }
        public int TotalItems { get; set; }

    }
}
