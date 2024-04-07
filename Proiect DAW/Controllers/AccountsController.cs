using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proiect_DAW.DTOs;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AccountsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] AccountDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(request.Password != request.ConfirmPassword)
            {
                return BadRequest("Parolele nu coincid!");
            }

            var account = new Account()
            {
                UserName = request.UserName,
                Password = request.Password,
                Admin = request.Admin,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                RegisterDate = DateTime.UtcNow,
                Pinatas = 0,
            };
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return Ok(account);
        }

        //login
        [HttpPost("login")]
        public ActionResult Login([FromBody] Login login)
        {
            var user = _dbContext.Accounts.FirstOrDefault(x => (x.UserName == login.UserName && x.Password == login.Password));

            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Nume sau parola gresite!");
            }
        }

        //Create
        [HttpPost("register")]
        public ActionResult Register([FromBody] AccountDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _dbContext.Accounts.FirstOrDefault(p => p.UserName == request.UserName);
            if (user != null)
                return BadRequest("Utilizatorul deja exista!");
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Parolele nu corespund!");

            var account = new Account()
            {
                UserName = request.UserName,
                Password = request.Password,
                Admin = request.Admin,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                RegisterDate = DateTime.UtcNow,
            };
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return Ok(account);
        }

        //Edit
        [HttpPut]
        public ActionResult Edit(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountInDb = _dbContext.Accounts.Find(account.Id);
            if (accountInDb == null)
                return NotFound($"Contul cu Id = {account.Id} nu exista!");

            _dbContext.Entry(accountInDb).CurrentValues.SetValues(account);

            _dbContext.SaveChanges();
            return Ok(account);
        }

        //Edit
        [HttpPut("pinata-number")]
        public ActionResult EditPinata(int id, int pinata)
        {
            if (id == 0 || pinata < 1)
                BadRequest();

            var account = _dbContext.Accounts.Find(id);
            if (account == null)
                return NotFound($"Contul cu Id = {id} nu exista!");

            account.Pinatas = pinata;
            _dbContext.Update(account);

            _dbContext.SaveChanges();
            return Ok(id);
        }

        //Edit
        [HttpPut("add-pinata")]
        public ActionResult AddPinata(int id)
        {
            if (id <= 0)
                BadRequest();

            var account = _dbContext.Accounts.Find(id);
            if (account == null)
                return NotFound($"Contul cu Id = {id} nu exista!");

            account.Pinatas = account.Pinatas+1;
            _dbContext.Update(account);

            _dbContext.SaveChanges();
            return Ok(id);
        }

        //Get
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = _dbContext.Accounts.Find(id);

            if (result == null)
                return NotFound($"Contul cu Id = {id} nu exista!");

            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _dbContext.Accounts.Find(id);
            if (result == null)
                return NotFound();

            _dbContext.Accounts.Remove(result);
            _dbContext.SaveChanges();

            return NotFound($"Contul cu Id = {id} nu exista!");
        }

        //Get all
        [HttpGet("get-all-accounts")]
        public ActionResult GetAll()
        {
            var result = _dbContext.Accounts.ToList();

            return Ok(result);
        }
    }
}
