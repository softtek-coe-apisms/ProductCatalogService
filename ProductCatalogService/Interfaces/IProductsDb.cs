using ProductCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogService.Interfaces
{
    interface IProductsDb
    {
        ProductDTO Insert(ProductDTO product);
        ProductDTO SelectById(string idProduct);
        PageDTO SelectByName(string name);
        PageDTO SelectPage(int page, int numItems);
        bool Update(ProductDTO product);
        bool Delete(string idProduct);
    }
}
