using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kroger.Repositories;
using Kroger.Models;

namespace Kroger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Gets today's information for all tracked products
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var repo = new ProductRepository();
            var firstProduct = repo.GetAllProducts();
            return firstProduct;
        }

        // Gets today's information for a single product
        [HttpGet("{productId}")]
        public Product GetSingleProductInformation(string productId)
        {
            var repo = new ProductRepository();
            return repo.GetSingleProductInformation(productId);
        }

        //Gets a product's all time maximum price
        [HttpGet("{productId}/max")]
        public float GetMaximumPriceByProduct(string productId)
        {
            var repo = new ProductRepository();
            return repo.GetMaximumPriceByProduct(productId);
        }

        //Gets a product's all time minimum price
        [HttpGet("{productId}/min")]
        public float GetMinimumPriceByProduct(string productId)
        {
            var repo = new ProductRepository();
            return repo.GetMinimumPriceByProduct(productId);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
