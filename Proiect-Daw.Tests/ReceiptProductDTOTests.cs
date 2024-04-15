using System.ComponentModel.DataAnnotations;
using Proiect_DAW.DTOs;

namespace Proiect_Daw.Tests
{
    public class ReceiptProductDTOTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void ReceiptProductDTO_WithValidData_ShouldPassValidation()
        {
            var receiptProductDto = new ReceiptProductDTO
            {
                ProductId = 1,
                ReceiptId = 1,
                Amount = 2
            };

            var validationContext = new ValidationContext(receiptProductDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(receiptProductDto, validationContext, validationResults, true);

            NUnit.Framework.Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        
        [Test]
        public void ReceiptProductDTO_WithInvalidAmount_ShouldFailValidation()
        {
            var receiptProductDto = new ReceiptProductDTO
            {
                ProductId = 1,
                ReceiptId = 1,
                Amount = 0
            };

            var validationContext = new ValidationContext(receiptProductDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(receiptProductDto, validationContext, validationResults, true);
            
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.AreEqual("Amount must be a positive value.", validationResults[0].ErrorMessage);
        }
        
        [Test]
        public void ReceiptProductDTO_WithBareMinimumData_ShouldPassValidation()
        {
            var receiptProductDto = new ReceiptProductDTO
            {
                ProductId = 1,
                ReceiptId = 1,
                Amount = 1
            };

            var validationContext = new ValidationContext(receiptProductDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(receiptProductDto, validationContext, validationResults, true);

            NUnit.Framework.Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        
        [Test]
        public void ReceiptProductDTO_WithSlightlyAboveMinimumData_ShouldPassValidation()
        {
            var receiptProductDto = new ReceiptProductDTO
            {
                ProductId = 1,
                ReceiptId = 1,
                Amount = 2
            };

            var validationContext = new ValidationContext(receiptProductDto, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(receiptProductDto, validationContext, validationResults, true);

            NUnit.Framework.Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
    }
}