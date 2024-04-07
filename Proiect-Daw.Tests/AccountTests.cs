using Proiect_DAW.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Daw.Tests
{
    public class AccountDTOTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AccountDTO_WithValidData_ShouldPassValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = "ValidUser",
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123",
                Admin = false,
                LastName = "Doe",
                FirstName = "John",
                PhoneNumber = "1234567890"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            NUnit.Framework.Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        [Test]
        public void AccountDTO_WithInvalidUserName_ShouldFailValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = new string('a', 51), // UserName too long
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123",
                Admin = false,
                LastName = "Doe",
                FirstName = "John",
                PhoneNumber = "1234567890"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.AreEqual("Username cannot exceed 50 characters.", validationResults[0].ErrorMessage);
        }
    }
}