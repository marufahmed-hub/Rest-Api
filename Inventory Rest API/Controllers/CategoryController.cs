using Inventory_Rest_API.Attributes;
using Inventory_Rest_API.Models;
using Inventory_with_Repository_Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Inventory_Rest_API.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        //HAETOAS-Hypermedia As The Engine Of Application State
        //Basic Authentication
        //Open/Token-based Authentication
        //Third Party Authentication

        CategoryRepository categoryRepository = new CategoryRepository();

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(categoryRepository.GetAll());
        }
        [Route("{id}",Name ="GetCategoryById")]
        public IHttpActionResult Get(int id)
        {
            
            var category = categoryRepository.Get(id);
            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            category.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "GET", Relation = "Self" });
            category.Links.Add(new Link() { Url = "http://localhost:56953/api/categories", Method = "GET", Relation = "Get all categories" });
            category.Links.Add(new Link() { Url = "http://localhost:56953/api/categories", Method = "POST", Relation = "Create new category resource" });
            category.Links.Add(new Link() { Url = "http://localhost:56953/api/categories/2", Method = "PUT", Relation = "Modify existing category resource" });
            category.Links.Add(new Link() { Url = "http://localhost:56953/api/categories/2", Method = "DELETE", Relation = "Remove existing category resource" });
            return Ok(categoryRepository.Get(id));
        }
        [Route("")]
        public IHttpActionResult Post(Category category)
        {
            categoryRepository.Insert(category);
            string uri = Url.Link("GetCategoryById",new { id=category.CategoryId});
            return Created(uri,category);
        }
        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id,[FromBody]Category category)
        {
            category.CategoryId = id;
            categoryRepository.Update(category);
            return Ok(category);
        }
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}/products")]
        public IHttpActionResult GetProductsBycategoryId(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return Ok(productRepository.GetProductsByCategory(id));
        }
    }
}
