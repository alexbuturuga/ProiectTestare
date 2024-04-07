using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.DTOs;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    [Route("api/account-promotion")]
    [ApiController]
    public class AccountPromotionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AccountPromotionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] AccountPromotionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountPromotion = new AccountPromotion()
            {
                AccountId = request.AccountId,
                PromotionId = request.PromotionId,
            };
            _dbContext.AccountPromotions.Add(accountPromotion);
            _dbContext.SaveChanges();
            return Ok(accountPromotion);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(AccountPromotion accountPromotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountPromotionInDb = _dbContext.AccountPromotions.Find(accountPromotion.Id);
            if (accountPromotionInDb == null)
                return new JsonResult(NotFound($"Produsul cu Id = {accountPromotion.Id} nu exista"));

            _dbContext.Entry(accountPromotionInDb).CurrentValues.SetValues(accountPromotion);

            _dbContext.SaveChanges();
            return Ok(accountPromotion);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.AccountPromotions.Find(id);

            if (result == null)
                return NotFound($"Produsul cu Id = {id} nu exista");

            return Ok(result);
        }

        //Get
        [HttpGet("user-promotions")]
        public ActionResult GetPromo(int idUser)
        {
            var Offers = _dbContext.AccountPromotions
                .Include(p=>p.Promotion)
                .Where(p => p.AccountId == idUser)
                .Select(p => new {p.Id, p.Promotion.PromotionDescription, p.Promotion.Discount}).ToList();

            if (Offers == null)
                return NotFound($"Nu exista promotii");

            return Ok(Offers);
        }

        //Get
        [HttpGet("drepturi")]
        public ActionResult GetDrepturi(int id)
        {
            var Offers = _dbContext.Accounts.Where(p => p.Id == id)
                                            .Select(p => p.Admin)
                                            .FirstOrDefault();

            return Ok(Offers);
        }
        //Delete
        [HttpDelete("promotionaccount")]
        public ActionResult Delete(int idAccount, int idPromotion)
        {
            var result = _dbContext.AccountPromotions.Where(p => p.AccountId == idAccount).ToList();
           
            if (result == null)
                return NotFound($"Promotia nu exista");

            var res = result.Where(p => p.Id == idPromotion).FirstOrDefault();

            if (res == null)
                return NotFound($"Promotia nu exista");

            _dbContext.AccountPromotions.Remove(res);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.AccountPromotions.Find(id);
            if (result == null)
                return NotFound($"Produsul cu Id = {id} nu exista");

            _dbContext.AccountPromotions.Remove(result);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //Get all
        [HttpGet("get-all-accountPromotions")]
        public ActionResult GetAll()
        {
            var result = _dbContext.AccountPromotions.ToList();

            return Ok(result);
        }
    }
}
