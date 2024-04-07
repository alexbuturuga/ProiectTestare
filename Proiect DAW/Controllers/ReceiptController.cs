using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.DTOs;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    [Route("api/receipt")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReceiptController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] ReceiptDTO request)
        {
            int discount = 0;
            if (request.PromotionId != null)
            {
                var discountS = _dbContext.AccountPromotions
                    .Include(p => p.Promotion)
                    .Where(p => p.Id == request.PromotionId)
                    .FirstOrDefault();
                if (discountS != null)
                    discount = discountS.Promotion.Discount;
                else
                    return BadRequest("Codul de promotie este gresit!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var receipt = new Receipt();
            if (discount != 0)
            {
                receipt = new Receipt()
                {
                    Date = DateTime.Now,
                    Sum = request.Sum - (request.Sum * discount) / 100,
                    ReceiptDescription = request.ReceiptDescription + " " + discount + "% reducere",
                    AccountId = request.AccountId,
                };
            } else
            {
                receipt = new Receipt()
                {
                    Date = DateTime.Now,
                    Sum = request.Sum - (request.Sum * discount) / 100,
                    ReceiptDescription = request.ReceiptDescription,
                    AccountId = request.AccountId,
                };
            }

            _dbContext.Receipts.Add(receipt);
            _dbContext.SaveChanges();

            
            return Ok(receipt);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productInDb = _dbContext.Products.Find(receipt.Id);
            if (productInDb == null)
                return NotFound($"Chitanta cu Id = {receipt.Id} nu exista!");

            _dbContext.Entry(productInDb).CurrentValues.SetValues(receipt);

            _dbContext.SaveChanges();
            return Ok(receipt);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.Receipts.Find(id);

            if (result == null)
                return NotFound($"Chitanta cu Id = {id} nu exista!");

            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.Receipts.Find(id);
            if (result == null)
                return NotFound($"Chitanta cu Id = {id} nu exista!");

            _dbContext.Receipts.Remove(result);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Get all
        [HttpGet("get-all-receipts")]
        public ActionResult GetAll()
        {
            var result = _dbContext.Receipts.ToList();

            return Ok(result);
        }
    }
}
