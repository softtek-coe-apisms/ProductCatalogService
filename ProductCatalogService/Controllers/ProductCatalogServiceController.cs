using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Interfaces;
using ProductCatalogService.Models;
using ProductCatalogService.Models.EF;

namespace ProductCatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalogServiceController : ControllerBase, ICatalog

    {
        private IProductsDb db;

        public ProductCatalogServiceController(bool mock = false)
        {
            if (mock)
                db = new DataProductsMock();
            else
                db = new DataProducts();
        }

        //GET api/productCatalogService/{id}
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetById(string id)
        {
            try
            {
                return Ok(db.SelectById(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/productCatalogService? pageNumber = { numPage }
        [Route("")]
        [HttpGet]
        public ActionResult GetPage(int? pageNumber, string name)
        {
            try
            {
                if (pageNumber.HasValue && name != null)
                    return Ok(db.SelectByName(name, pageNumber.Value, 15));
                else if (name != null)
                    return Ok(db.SelectByName(name, 1, 15));
                else if (pageNumber.HasValue)
                    return Ok(db.SelectPage(pageNumber.Value, 15));
                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == DataProducts.NO_PRODUCTS_FOUND)
                    return Ok(new PageDTO());
                else if (e.Message == DataProducts.PAGE_NOT_EXIST)
                    return Ok(new PageDTO());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //POST api/productCatalogService
        [Route("")]
        [HttpPost]
        public ActionResult PostProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                return Ok(db.Insert(productDTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //PUT api/productCatalogService
        [Route("")]
        [HttpPut]
        public ActionResult PutProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                if (db.Update(productDTO))
                    return Ok();
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //DELETE api/productCatalogService/{id}
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            try
            {
                if (db.Delete(id))
                    return Ok();
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}