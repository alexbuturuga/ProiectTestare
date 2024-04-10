using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW;
using Proiect_DAW.Controllers;
using Proiect_DAW.DTOs;
using Proiect_DAW.Models;

namespace Proiect_Daw.Tests;

public class PromotionControllerTests
{
    [Test]
        public void Create_Returns_Ok_When_ValidObject()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var promotionDto = new PromotionDTO { PromotionDescription = "Promotion 1", Discount = 1 };
            
            var result = controller.Create(promotionDto) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result?.Value?.GetType(), Is.EqualTo(typeof(Promotion)));
        }

        [Test]
        public void Edit_Returns_Ok_When_ValidObject()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var promotion = new Promotion { PromotionId = 1, PromotionDescription = "Promotion 1", Discount = 1 };
            dbContext.Promotions.Add(promotion);
            dbContext.SaveChanges();
            promotion.PromotionDescription = "Updated Description";
            
            var result = controller.Edit(promotion) as OkObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result?.Value?.GetType(), Is.EqualTo(typeof(Promotion)));
            Assert.That((result?.Value as Promotion)?.PromotionDescription, Is.EqualTo("Updated Description"));
        }

        [Test]
        public void Get_Returns_Ok_When_PromotionExists()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var promotion = new Promotion { PromotionId = 1, PromotionDescription = "Promotion 1", Discount = 1 };
            dbContext.Promotions.Add(promotion);
            dbContext.SaveChanges();
            
            var result = controller.Get(1) as OkObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result?.Value?.GetType(), Is.EqualTo(typeof(Promotion)));
        }

        [Test]
        public void Get_Returns_NotFound_When_PromotionNotFound()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            
            var result = controller.Get(1) as NotFoundObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetRandom_Returns_Ok_When_UserExists()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var account = new Account { Id = 1, UserName = "Vlad1", Password = "Vlad1", Admin = false, LastName = "Oancea", FirstName = "Vlad", PhoneNumber = "0777777777" };
            var promotion1 = new Promotion { PromotionId = 1, PromotionDescription = "Promotion 1", Discount = 1 };
            var promotion2 = new Promotion { PromotionId = 2, PromotionDescription = "Promotion 2", Discount = 2 };
            var promotion3 = new Promotion { PromotionId = 3, PromotionDescription = "Promotion 3", Discount = 3 };
            dbContext.Accounts.Add(account);
            dbContext.Promotions.AddRange(promotion1, promotion2, promotion3);
            dbContext.SaveChanges();
            
            var result = controller.GetRandom(1) as OkObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result?.Value?.GetType(), Is.EqualTo(typeof(Promotion)));
        }

        [Test]
        public void GetRandom_Returns_NotFound_When_UserNotFound()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            
            var result = controller.GetRandom(1) as NotFoundObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void Delete_Returns_NoContent_When_PromotionDeleted()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var promotion = new Promotion { PromotionId = 1, PromotionDescription = "Promotion 1", Discount = 1 };
            dbContext.Promotions.Add(promotion);
            dbContext.SaveChanges();
            
            var result = controller.Delete(1) as NoContentResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public void Delete_Returns_NotFound_When_PromotionNotFound()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            
            var result = controller.Delete(1) as NotFoundObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetAll_Returns_Ok_With_AllPromotions()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            var controller = new PromotionController(dbContext);
            var promotions = new List<Promotion> {
                new Promotion { PromotionId = 1, PromotionDescription = "Promotion 1", Discount = 1 },
                new Promotion { PromotionId = 2, PromotionDescription = "Promotion 2", Discount = 2 },
                new Promotion { PromotionId = 3, PromotionDescription = "Promotion 3", Discount = 3 }
            };
            dbContext.Promotions.AddRange(promotions);
            dbContext.SaveChanges();
            
            var result = controller.GetAll() as OkObjectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result?.Value?.GetType(), Is.EqualTo(typeof(List<Promotion>)));
            var resultList = result?.Value as List<Promotion>;
            Assert.That(resultList?.Count, Is.EqualTo(promotions.Count));
        }
}