using Proiect_DAW.DTOs;
using Proiect_DAW.Models;
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

        [Test]
        public void CalculatePinatas_Should_Calculate_Correctly_With_Bonus()
        {
            var account = new Account();
            account.CalculatePinatas(100, 2, 5); // 100 unități, coeficient 2, bonus 5
            Assert.AreEqual(25, account.Pinatas); // Așteptăm 25 de pinatas ((100 / 10 * 2) + 5)
        }

        [Test]
        public void CalculatePinatas_Should_Throw_Exception_For_Invalid_Arguments()
        {
            var account = new Account();
            // Verificăm dacă aruncă o excepție când valori sunt nevalide
            Assert.Throws<ArgumentException>(() => account.CalculatePinatas(0, 5, 2));
            Assert.Throws<ArgumentException>(() => account.CalculatePinatas(50, 0, 1));
        }

        [Test]
        public void CalculatePinatas_Should_Handle_Negative_Bonus()
        {
            var account = new Account();
            account.CalculatePinatas(100, 2, -5); // 100 unități, coeficient 2, bonus negativ -5
            Assert.AreEqual(15, account.Pinatas); // Așteptăm 15 pinatas ((100 / 10 * 2) - 5)
        }
    }
}