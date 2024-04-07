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
                UserName = "George123",
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
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
                UserName = new string('g', 51), // UserName too long
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);
            
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.AreEqual("Username cannot exceed 50 characters.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void AccountDTO_WithInvalidPassword_ShouldFailValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = "George123",
                Password = "cal", // Parola prea scurtă
                ConfirmPassword = "cal",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual("Password must be at least 8 characters long.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void AccountDTO_WithInvalidPhoneNumber_ShouldFailValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = "George123",
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "12345" // Număr de telefon invalid
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual("Phone number must be 10 digits.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void AccountDTO_WithMaxLengthUserName_ShouldPassValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = new string('g', 50), // UserName exact la limita maximă
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        [Test]
        public void AccountDTO_WithJustBelowMaxUserName_ShouldPassValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = new string('a', 49), // UserName sub limita maximă
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        [Test]
        public void AccountDTO_WithJustAboveMaxUserName_ShouldFailValidation()
        {
            var accountDto = new AccountDTO
            {
                UserName = new string('g', 51), // UserName peste limita maximă
                Password = "Broasca55",
                ConfirmPassword = "Broasca55",
                Admin = false,
                LastName = "Buturuga",
                FirstName = "George",
                PhoneNumber = "0721333444"
            };

            var validationContext = new ValidationContext(accountDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(accountDto, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual("Username cannot exceed 50 characters.", validationResults[0].ErrorMessage);
        }
    }
}