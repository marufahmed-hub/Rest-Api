using Inventory_Rest_API.Models;
using Inventory_with_Repository_Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory_Rest_API.Controllers
{
    public class ProductsController : ApiController
    {
        ProductRepository productRepository = new ProductRepository();
        public IHttpActionResult Get()
        {
            return Ok(productRepository.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            var product = productRepository.Get(id);
            if (product == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(productRepository.Get(id));
        }

        public IHttpActionResult Post(Product product)
        {
            productRepository.Insert(product);
            return Created("api/products/" + product.ProductId, product);
        }

        public IHttpActionResult Put([FromUri] int id, [FromBody] Product product)
        {
            product.ProductId = id;
            productRepository.Update(product);
            return Ok(product);
        }
        public IHttpActionResult Delete(int id)
        {
            productRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
