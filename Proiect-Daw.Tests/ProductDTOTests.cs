using Proiect_DAW.DTOs;
using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Daw.Tests
{
    
    public class ProductDTOTests
    {
        private ProductDTO product;

        [SetUp]
        public void Setup()
        {
            product = new ProductDTO();
            // Initializare cu valori valide pentru a evita erorile din alte proprietăți
            product.ProductName = "Cafea cu lapte";
            product.ProductDescription = "Cafea cu lapte si spuma";
            product.Price = 10.0; // Presupunem că este o valoare validă
            product.Cover = "https://www.pexels.com/photo/close-up-of-coffee-cup-on-table-312418/"; // URL valid
            product.Rating = 3; // Rating valid
        }


        // PARTITIONARE IN CLASE DE ECHIVALENTA

        // NUME VALID
        [Test]
        public void TestProductName_Valid_ShouldPass()
        {
            product.ProductName = new string('g', 40);

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);

        }
 

        // NUME INVALID - depaseste limita de 50
        [Test]
        public void TestProductName_TooLong_ShouldFail()
        {
            // Arrange
            var product = new ProductDTO();
            // Initializare cu valori valide pentru a evita erorile din alte proprietăți
            product.ProductName = new string('g', 61);
           
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //NUME EMPTY
        [Test]
        public void TestProductName_Empty_ShouldFail()
        {
            
            // Initializare cu valori valide pentru a evita erorile din alte proprietăți
            product.ProductName = "";
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //NUME NULL
        [Test]
        public void TestProductName_Null_ShouldFail()
        {
            
            // Initializare cu valori valide pentru a evita erorile din alte proprietăți
            product.ProductName = null;
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults); // Ar trebui să conțină cel puțin un rezultat de eroare
        }

        //DESCRRIERE VALIDA
        [Test]
        public void ProductDescription_Valid_ShouldPass()
        {
            // Arrange
            product.ProductDescription = new string('a', 100); // 199 de caractere

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }


        // DESCRIERE PREA MARE
        [Test]
        public void ProductDescription_TooBig_ShouldFail()
        {
            // Arrange
            product.ProductDescription = new string('a', 400); // 201 de caractere

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Some.Matches<ValidationResult>(vr => vr.ErrorMessage.Contains("Product Description cannot exceed 200 characters.")));
        }

        // DESCRIERE FIX 200
        [Test]
        public void ProductDescription_Valid200_ShouldPass()
        {
            // Arrange
            product.ProductDescription = new string('a', 200); // 199 de caractere

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        // DESCRIERE FRONTIERA 199
        [Test]
        public void ProductDescription_Valid199_ShouldPass()
        {
            // Arrange
            product.ProductDescription = new string('a', 199); // 199 de caractere

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // DESCRIERE FRONTIERA 201
        [Test]
        public void ProductDescription_Invalid201_ShouldFail()
        {
            // Arrange
            product.ProductDescription = new string('a', 201); // 201 de caractere

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //DESCRIERE EMPTY
        [Test]
        public void ProductDescription_Empty_ShouldPass()
        {
            // Arrange
            product.ProductDescription = ""; // empty

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.IsTrue(isValid, "Expect the validation to pass for an empty description.");
            Assert.IsEmpty(validationResults, "Expected no validation error for an empty description.");
        }

        // PRET VALID
        [Test]
        public void ProductPrice_Valid_ShouldPass()
        {
            product.Price = 20.0; //valid price
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsTrue(isValid, "Expect the validation to pass for a positive price.");
            Assert.IsEmpty(validationResults, "Expects no validation error for valid price");
        }

        // PRET NEGATIV
        [Test]
        public void ProductPrice_NegativeValue_ShouldFail()
        {
            product.Price = -20.0; //invalid price
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid, "Expect the validation to fail for a negative price.");
            Assert.IsNotEmpty(validationResults, "Expects validation errors for negative price");
        }

        // PRET 0
        [Test]
        public void ProductPrice_ZeroValue_ShouldFail()
        {
            product.Price = 0.0; //invalid price
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid, "Expect the validation to fail for price = 0.");
            Assert.IsNotEmpty(validationResults, "Expects validation errors price = 0");
        }

        // URL VALID POZA
        [Test]
        public void ProductPicture_ValidURL_ShouldPass()
        {
            product.Cover = "https://www.pexels.com/collections/coffee-culture-ffaz4w5/";
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        // URL INVALID POZA
        [Test]
        public void ProductPicture_InvalidURL_ShouldFail()
        {
            product.Cover = "http:/]imnniidnenieuii";
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //URL EMPTY
        [Test]
        public void ProductPicture_EmptyURL_ShouldFail()
        {
            product.Cover = "";
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //URL EMPTY
        [Test]
        public void ProductPicture_NullURL_ShouldFail()
        {
            product.Cover = null;
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
        }

        //RATING VALID
        [Test]
        public void ProductRating_Valid_ShouldPass()
        {
            product.Rating = 3; //valid rating
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        //RATING VALID - 0
        [Test]
        public void ProductRating_ZeroValue_ShouldPass()
        {
            product.Rating = 0; //valid rating
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        //RATING VALID - 5
        [Test]
        public void ProductRating_5Value_ShouldPass()
        {
            product.Rating = 5;//valid rating
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
        //RATING INVALID - VALOARE NEGATIVA 
        [Test]
        public void ProductRating_NegativeValue_ShouldFail()
        {
            product.Rating = -1; //invalid price
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);
     
        }
        //RATING INVALID - 6
        [Test]
        public void ProductRating_EmptyValue_ShouldFail()
        {
            product.Rating = 6; //invalid price
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsNotEmpty(validationResults);

        }





    }
}
