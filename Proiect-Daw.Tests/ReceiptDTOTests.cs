using Proiect_DAW.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;


namespace Proiect_Daw.Tests
{
    public class ReceiptDTOTests
    {
        private ReceiptDTO receipt;
        [SetUp]
        public void Setup()
        {
            receipt = new ReceiptDTO();

            receipt.Sum = 20;
            receipt.ReceiptDescription = "Descriere valida";
            receipt.AccountId = 1;
            receipt.PromotionId = 1;
        }

        // Equivalence partitioning + Boundary Analysis

        // SUM - Valid
        [Test]
        public void TestReceiptSum_Valid_ShouldPass()
        {
            receipt.Sum = 20;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // SUM - Invalid (less than 0)
        [Test]
        public void TestReceiptSum_Negative_ShouldFail()
        {
            receipt.Sum = -15.1;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        // SUM - Invalid (empty)
        [Test]
        public void TestReceiptSum_Empty_ShouldFail()
        {
            receipt.Sum = double.NaN;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }


        // SUM - Valid (Boundary Analysis - 0)
        [Test]
        public void TestReceiptSum_Zero_ShouldPass()
        {
            receipt.Sum = 0;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // SUM - Invalid (Boundary Analysis - -1)
        [Test]
        public void TestReceiptSum_MinusOne_ShouldFail()
        {
            receipt.Sum = -1;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        // SUM - Valid (Boundary Analysis - Max)
        [Test]
        public void TestReceiptSum_Max_ShouldPass()
        {
            receipt.Sum = double.MaxValue;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIPTION - Valid
        [Test]
        public void TestReceiptDescription_Valid_ShouldPass()
        {
            receipt.ReceiptDescription = "Descriere scurta";

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIPTION - Valid (Null)
        [Test]
        public void TestReceiptDescription_Null_ShouldPass()
        {
            receipt.ReceiptDescription = null;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIPTION - Valid (empty)
        [Test]
        public void TestReceiptDescription_Empty_ShouldPass()
        {
            receipt.ReceiptDescription = "";

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIPTION - Valid (Boundary analysis - 200 characters)
        [Test]
        public void TestReceiptDescription_200Characters_ShouldPass()
        {
            receipt.ReceiptDescription = new string('a', 200);

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIPTION - Invalid (Boundary analysis - 201 characters)
        [Test]
        public void TestReceiptDescription_201Characters_ShouldFail()
        {
            receipt.ReceiptDescription = new string('a', 201);

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        // DESCRIPTION - Invalid (Too long)
        [Test]
        public void TestReceiptDescription_TooLong_ShouldFail()
        {
            receipt.ReceiptDescription = new string('a', 250);

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        // ACCOUNTID - Valid
        [Test]
        public void TestReceiptAccountId_Valid_ShouldPass()
        {
            receipt.AccountId = 1;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // PROMOTIONID - Valid (value provided)
        [Test]
        public void TestReceiptPromotionId_Provided_ShouldPass()
        {
            receipt.PromotionId = 1;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // PROMOTIONID - Valid (value not provided)
        [Test]
        public void TestReceiptPromotionId_NotProvided_ShouldPass()
        {
            receipt.PromotionId = null;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(receipt, new ValidationContext(receipt), validationResults, true);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // STATEMENT COVERAGE
        [Test]
        public void TestReceipt_StatementCoverage()
        {
            receipt.Sum = 10.5;
            receipt.ReceiptDescription = "Test description";
            receipt.AccountId = 2;
            receipt.PromotionId = null;

            Assert.AreEqual(10.5, receipt.Sum);
            Assert.AreEqual("Test description", receipt.ReceiptDescription);
            Assert.AreEqual(2, receipt.AccountId);
            Assert.IsNull(receipt.PromotionId);
        }
    }   
}
