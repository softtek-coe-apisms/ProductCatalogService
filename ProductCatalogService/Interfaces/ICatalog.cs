using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogService.Interfaces
{
    /// <summary>
    /// Se Crea Intefraz y se Agregan Metodos: GBId y GBname.
    /// </summary>
    interface ICatalog
    {
                
        ActionResult GetById(string id); 
        ActionResult GetPage(int? pageNumber, string name);
        
    }
}
