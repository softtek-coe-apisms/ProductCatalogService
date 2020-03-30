using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogService.Models
{
    public class ProductDTO
    {

        /*id
         name
         description
         picture
         priceusd=manda llamar clase
         categories*/
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public PriceDTO PriceUsd { get; set; }
        public List<string> Categories { get; set; }

    }
}
