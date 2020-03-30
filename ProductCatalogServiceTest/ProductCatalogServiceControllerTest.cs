using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalogService.Controllers;
using ProductCatalogService.Models;
using System;
using System.Collections.Generic;

namespace ProductCatalogServiceTest
{
    [TestClass]
    public class ProductCatalogServiceControllerTest
    {
        ProductCatalogServiceController pcscMock;

        [TestInitialize]
        public void Initialize()
        {
            pcscMock = new ProductCatalogServiceController(true);
        }

        [TestMethod]
        public void GetById_ValidId_ProductDTO()
        {
            //Arrange
            string idProduct = "xDgnEowe4u";
            ProductDTO actual = new ProductDTO();

            //Act
            try
            {
                actual = (ProductDTO)((OkObjectResult)pcscMock.GetById(idProduct)).Value;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.AreNotEqual(null, actual);
        }

        [TestMethod]
        public void GetPage_ValidPageNumber_PageDTO()
        {
            //Arrange
            int pageNumber = 1;
            PageDTO actual = new PageDTO();

            //Act
            try
            {
                actual = (PageDTO)((OkObjectResult)pcscMock.GetPage(pageNumber, "")).Value;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void PostProduct_ValidProductDTO_ProductWithValidId()
        {
            //Arrange
            ProductDTO product = new ProductDTO
            {
                Id = Utilities.NewProductId(10, false),
                Categories = new List<string> { "SAVAGE", "EXTREME" },
                Description = "short desctription",
                Name = "teddy",
                Picture = "https://images.unsplash.com/photo-1544719576-904e2d01e057?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjExNjU4M30",
                PriceUsd = new PriceDTO { CurrencyCode = "USD", Units = 23412, Nanos = 234 }
            };
            ProductDTO actual = new ProductDTO();

            //Act
            try
            {
                actual = (ProductDTO)((OkObjectResult)pcscMock.PostProduct(product)).Value;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.AreEqual(product.Id.Length, actual.Id.Length);
        }

        [TestMethod]
        public void PutProduct_ValidProductDTO_Ok200StatusCode()
        {
            //Arrange
            ProductDTO product = new ProductDTO
            {
                Id = "xDgnEowe4u",
                Categories = new List<string> { "SAVAGE", "EXTREME" },
                Description = "short desctription",
                Name = "teddy",
                Picture = "https://images.unsplash.com/photo-1544719576-904e2d01e057?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjExNjU4M30",
                PriceUsd = new PriceDTO { CurrencyCode = "USD", Units = 23412, Nanos = 234 }
            };
            int statusCodeExpected = 200;
            int actual = 0;

            //Act
            try
            {
                actual = ((OkResult)pcscMock.PutProduct(product)).StatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.AreEqual(statusCodeExpected, actual);
        }

        [TestMethod]
        public void Delete_ValidProductId_Ok200StatusCode()
        {
            //Arrange
            string validProductId = "xDgnEowe4u";
            int statusCodeExpected = 200;
            int actual = 0;

            //Act
            try
            {
                actual = ((OkResult)pcscMock.Delete(validProductId)).StatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.AreEqual(statusCodeExpected, actual);
        }
    }
}
