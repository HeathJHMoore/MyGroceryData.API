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
        [HttpGet("{firebaseId}/home")]
        public IEnumerable<Product> GetAllProducts(string firebaseId)
        {
            var repo = new ProductRepository();
            var firstProduct = repo.GetAllProducts(firebaseId);
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

        [HttpGet("{firebaseId}/{productId}/details")]
        public ProductDetails GetProductDetails(string firebaseId, string productId)
        {
            var repo = new ProductRepository();
            return repo.GetProductSummaryInformation(firebaseId, productId);
        }

        [HttpGet("{firebaseId}/{productId}/{startDate}/seven-day-trend")]
        public IEnumerable<object> GetSevenDayTrend(string firebaseId, string productId, string startDate)
        {
            var repo = new ProductRepository();
            return repo.Get7DayPriceAction(firebaseId, productId, startDate);
        }

        // POST api/values
        [HttpPost("{firebaseId}/{productId}/add-to-watchlist")]
        public void AddToUserWatchlist(string firebaseId, int productId)
        {
            var repo = new ProductRepository();
            repo.AddToUserWatchlist(firebaseId, productId);
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
