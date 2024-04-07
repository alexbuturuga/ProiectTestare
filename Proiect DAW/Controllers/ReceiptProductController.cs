using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect_DAW.DTOs;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    [Route("api/receipt-product")]
    [ApiController]
    public class ReceiptProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReceiptProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] ReceiptProductDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var receiptProduct = new ReceiptProduct()
            {
                ReceiptId = request.ReceiptId,
                ProductId = request.ProductId,
                Amount = request.Amount,
            };
            _dbContext.ReceiptProducts.Add(receiptProduct);
            _dbContext.SaveChanges();
            return Ok(receiptProduct);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(ReceiptProduct receiptProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var receiptProductInDb = _dbContext.ReceiptProducts.Find(receiptProduct.Id);
            if (receiptProductInDb == null)
                return NotFound($"Chitanta cu Id = {receiptProduct.Id} nu exista!");

            _dbContext.Entry(receiptProductInDb).CurrentValues.SetValues(receiptProduct);

            _dbContext.SaveChanges();
            return Ok(receiptProduct);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.ReceiptProducts.Find(id);

            if (result == null)
                return NotFound($"Chitanta cu Id = {id} nu exista!");

            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.ReceiptProducts.Find(id);
            if (result == null)
                return NotFound($"Chitanta cu Id = {id} nu exista!");

            _dbContext.ReceiptProducts.Remove(result);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Get all
        [HttpGet("get-all-receiptProduct")]
        public ActionResult GetAll()
        {
            var result = _dbContext.ReceiptProducts.ToList();

            return Ok(result);
        }
    }
}
