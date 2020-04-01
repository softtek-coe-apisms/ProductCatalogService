using ProductCatalogService.Interfaces;
using ProductCatalogService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogService.Models
{
    public class DataProducts : DataSource, IProductsDb
    {
        public DataProducts()
        {
            db = new DataProductsContext();
        }

        public bool Delete(string idProduct)
        {
            try
            {
                var productInDb = db.Products.FirstOrDefault(p => p.Title == idProduct && p.IsEnabled == 1);
                if (productInDb != null)
                    productInDb.IsEnabled = 1;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ProductDTO Insert(ProductDTO productDTO)
        {
            try
            {
                //generate a new ID for the product
                string newId = Utilities.NewProductId(10, false);
                while (db.Products.FirstOrDefault(p => p.Title == newId) != null)
                    newId = Utilities.NewProductId(10, false);

                var productInserted = db.Products.Add(new Products
                {
                    Title = newId,
                    Nombre = productDTO.Name,
                    Description = productDTO.Description,
                    Picture = Utilities.GetImageByNameOrDefault(productDTO.Name),
                    CurrencyCode = USD,
                    PriceClient = productDTO.PriceUsd.Units,
                    MemberDiscount = 1,
                    Keywords = string.Join("-", productDTO.Categories).ToUpper(),
                    DateUpdate = DateTime.Now,
                    IsEnabled = 1
                }).Entity;
                db.SaveChanges();

                productDTO.Id = productInserted.Title;
                productDTO.Picture = productInserted.Picture;
                return productDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ProductDTO SelectById(string idProduct)
        {
            try
            {
                var productInDB = db.Products.FirstOrDefault(p => p.Title == idProduct && p.IsEnabled == 1);
                if (productInDB != null)
                    return new ProductDTO
                    {
                        Id = productInDB.Title,
                        Name = productInDB.Nombre,
                        Description = productInDB.Description,
                        Picture = productInDB.Picture,
                        PriceUsd = new PriceDTO
                        {
                            CurrencyCode = productInDB.CurrencyCode,
                            Units = int.Parse(productInDB.PriceClient.ToString().Split('.')[0]),
                            Nanos = int.Parse(productInDB.PriceClient.ToString().Split('.')[1])
                        },
                        Categories = productInDB.Keywords.Split("-").ToList()
                    };
                throw new Exception("Product not found");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PageDTO SelectByName(string name)
        {
            try
            {
                var ProductsInDb = db.Products
                    .Where(p => p.Nombre.Contains(name) && p.IsEnabled == 1)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Title,
                        Name = p.Nombre,
                        Description = p.Description,
                        Picture = p.Picture,
                        PriceUsd = new PriceDTO
                        {
                            CurrencyCode = p.CurrencyCode,
                            Units = int.Parse(p.PriceClient.ToString().Split('.', StringSplitOptions.None)[0]),
                            Nanos = int.Parse(p.PriceClient.ToString().Split('.', StringSplitOptions.None)[1])
                        },
                        Categories = p.Keywords.Split('-', StringSplitOptions.None).ToList()
                    }).ToList();
                if (ProductsInDb != null)
                    return new PageDTO
                    {
                        TotalItems = ProductsInDb.Count(),
                        Products = ProductsInDb
                    };
                throw new Exception("NO Products found where match the name");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PageDTO SelectPage(int page, int numItems)
        {
            try
            {
                int totalOfProducts = db.Products.Where(p => p.IsEnabled == 1).Count();
                int totalPages = totalOfProducts / numItems;
                if (totalOfProducts % numItems > 0)
                    totalPages++;

                if (page > totalPages)
                    return new PageDTO
					{
						TotalItems = totalPages,
						Products = new List<ProductCatalogService.Models.ProductDTO>()
					};

                var pageBuilded = db.Products
                    .Where(p => p.IsEnabled == 1)
                    .OrderBy(p => p.Id)
                    .Skip(--page * numItems)
                    .Take(numItems)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Title,
                        Name = p.Nombre,
                        Description = p.Description,
                        Picture = p.Picture,
                        PriceUsd = new PriceDTO
                        {
                            CurrencyCode = p.CurrencyCode,
                            Units = int.Parse(p.PriceClient.ToString().Split('.', StringSplitOptions.None)[0]),
                            Nanos = int.Parse(p.PriceClient.ToString().Split('.', StringSplitOptions.None)[1])
                        },
                        Categories = p.Keywords.Split('-', StringSplitOptions.None).ToList()
                    })
                    .ToList();
                return new PageDTO
                {
                    TotalItems = totalPages,
                    Products = pageBuilded
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(ProductDTO product)
        {
            try
            {
                var productOnDb = db.Products.FirstOrDefault(p => p.Title == product.Id && p.IsEnabled == 1);
                if (productOnDb != null)
                {
                    productOnDb.Nombre = product.Name;
                    productOnDb.Description = product.Description;
                    productOnDb.Picture = product.Picture;
                    productOnDb.CurrencyCode = USD;
                    productOnDb.PriceClient = decimal.Parse(product.PriceUsd.Units + "." + product.PriceUsd.Nanos);
                    productOnDb.MemberDiscount = 1;
                    productOnDb.Keywords = string.Join('-', product.Categories).ToUpper();

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
