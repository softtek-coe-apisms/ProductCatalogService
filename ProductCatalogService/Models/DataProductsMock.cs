using ProductCatalogService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerializeObjects;

namespace ProductCatalogService.Models
{
    public class DataProductsMock : DataSource, IProductsDb
    {
        private const string FILE_NAME = "DataProductsMock.xml";
        private const string FILE_PATH = @"C:\Users\Curso\source\repos\TeamUno\";
        private ProductDTO defaaultProduct;
        private List<ProductDTO> contextMock;

        public DataProductsMock()
        {
            defaaultProduct = new ProductDTO
            {
                Id = Utilities.NewProductId(10, false),
                Categories = new List<string> { "SAVAGE", "EXTREME" },
                Description = "short desctription",
                Name = "teddy",
                Picture = "https://images.unsplash.com/photo-1544719576-904e2d01e057?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjExNjU4M30",
                PriceUsd = new PriceDTO { CurrencyCode = "USD", Units = 23412, Nanos = 234 }
            };

            XMLFile.Path = FILE_PATH;
        }

        public bool Delete(string idProduct)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            contextMock.Remove(contextMock.FirstOrDefault(p => p.Id == idProduct));
            return true;
        }

        public ProductDTO Insert(ProductDTO product)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            product.Id = Utilities.NewProductId(10, false);
            contextMock.Add(product);
            XMLFile.SerializeList(contextMock, FILE_NAME);
            return product;
        }

        public ProductDTO SelectById(string idProduct)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            return contextMock.FirstOrDefault(p => p.Id == idProduct);
        }

        public PageDTO SelectByName(string name, int page, int numItems)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            var products = contextMock.Where(p => p.Name == name).ToList();
            if (products != null)
                return new PageDTO { TotalPages = 1, TotalProducts = products.Count(), Products = products };
            return new PageDTO();
        }

        public PageDTO SelectPage(int page, int numItems)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            return new PageDTO { TotalProducts = contextMock.Count(), TotalPages = 1, Products = contextMock };
        }

        public bool Update(ProductDTO product)
        {
            contextMock = XMLFile.DeserializeList<List<ProductDTO>>(FILE_NAME);

            var productInContext = contextMock.FirstOrDefault(p => p.Id == product.Id);
            if (productInContext != null)
            {
                productInContext = product;
                XMLFile.SerializeList(contextMock, FILE_NAME);
                return true;
            }
            return false;
        }
    }
}
