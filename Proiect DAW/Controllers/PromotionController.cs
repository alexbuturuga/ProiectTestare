using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Proiect_DAW.Models;
using Proiect_DAW.DTOs;
using Proiect_DAW.Migrations;

namespace Proiect_DAW.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PromotionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] PromotionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotion = new Promotion()
            {
                PromotionDescription = request.PromotionDescription,
                Discount = request.Discount,
            };
            _dbContext.Promotions.Add(promotion);
            _dbContext.SaveChanges();
            return Ok(promotion);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotionsInDb = _dbContext.Promotions.Find(promotion.PromotionId);
            if (promotionsInDb == null)
                return NotFound($"Promotia cu Id = {promotion.PromotionId} nu exista!");

            _dbContext.Entry(promotionsInDb).CurrentValues.SetValues(promotion);

            _dbContext.SaveChanges();
            return Ok(promotion);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.Promotions.Find(id);

            if (result == null)
                return NotFound($"Promotia cu Id = {id} nu exista!");

            return Ok(result);
        }

        //Get Random
        [HttpPost("random-and-give-to")]
        public ActionResult GetRandom(int id)
        {
            var account = _dbContext.Accounts.Find(id);

            if (account == null)
                return NotFound($"Userul cu Id = {id} nu exista!");

            Random r = new Random();
            var PromotionIds = _dbContext.Promotions.Select(x => x.PromotionId).ToList();

            var premiu = PromotionIds[r.Next(0, PromotionIds.Count()-1)];

            var result = _dbContext.Promotions.FirstOrDefault(p => p.PromotionId == premiu);

            _dbContext.AccountPromotions.Add(new AccountPromotion{AccountId = id, PromotionId = premiu});

            _dbContext.SaveChanges();
            
            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.Promotions.Find(id);
            if (result == null)
                return NotFound($"Promotia cu Id = {id} nu exista!");

            _dbContext.Promotions.Remove(result);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Get all
        [HttpGet("get-all-promotions")]
        public ActionResult GetAll()
        {
            var result = _dbContext.Promotions.ToList();

            return Ok(result);
        }
    }
}
