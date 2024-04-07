using Microsoft.AspNetCore.Mvc;
using Proiect_DAW.Models;
using Proiect_DAW.DTOs;

namespace Proiect_DAW.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] ProductDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 

            var product = new Product()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                Price = request.Price,
                Cover = request.Cover,
                Rating = request.Rating,
                AddDate = DateTime.UtcNow,
            };
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return Ok(product);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productInDb = _dbContext.Products.Find(product.ProductId);
            if (productInDb == null)
                return NotFound($"Product with Id = {product.ProductId} not found");

            _dbContext.Entry(productInDb).CurrentValues.SetValues(product);

            _dbContext.SaveChanges();
            return Ok(product);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.Products.Find(id);

            if (result == null)
                return NotFound($"Produsul cu Id = {id} nu exista!");

            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.Products.Find(id);
            if(result == null)
                return NotFound($"Produsul cu Id = {id} nu exista!");

            _dbContext.Products.Remove(result);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Get all
        [HttpGet("get-all-products")]
        public ActionResult GetAll() 
        {
            var result = _dbContext.Products.ToList();

            return Ok(result);
        }

        //Get new
        [HttpGet("new")]
        public ActionResult GetNew()
        {
            var result = _dbContext.Products
                                   .OrderByDescending(x => x.AddDate)
                                   .Take(5)
                                   .ToList();

            return Ok(result);
        }

        //Popularity
        [HttpGet("popular")]
        public ActionResult GetPopular()
        {
            var result = _dbContext.Products
                                   .OrderByDescending(x => x.Rating)
                                   .Take(5)
                                   .ToList();

            return Ok(result);
        }
    }
}
