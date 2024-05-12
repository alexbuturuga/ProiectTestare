using Proiect_DAW.Models;
using Xunit;

namespace Proiect_DAW.Tests
{
    public class AccountTests
    {
        private readonly Account _calculator;

        public AccountTests()
        {
            _calculator = new Account();
        }

        [Fact]
        public void CalculatePinatas_ValidInput_CalculatesCorrectly()
        {
            // Arrange
            int amount = 5;
            int multiplier = 5;
            int bonus = 5;
            int expected = 50; // 5 * (5 + 5) = 50

            // Act
            _calculator.CalculatePinatas(amount, multiplier, bonus);

            // Assert
            Xunit.Assert.Equal(expected, _calculator.Pinatas);
        }

        [Fact]
        public void CalculatePinatas_InvalidInput_ThrowsArgumentException()
        {
            // Arrange
            int amount = 0;
            int multiplier = 5;
            int bonus = 2;

            // Act & Assert
            Xunit.Assert.Throws<ArgumentException>(() => _calculator.CalculatePinatas(amount, multiplier, bonus));
        }

        [Fact]
        public void CalculatePinatas_BonusAppliedCorrectly()
        {
            // Arrange
            int amount = 20; // Every 10th pinata should get a bonus
            int multiplier = 5;
            int bonus = 2;
            int expected = 220; // (20 / 10) * (5 + 2) * 10 = 220

            // Act
            _calculator.CalculatePinatas(amount, multiplier, bonus);

            // Assert
            Xunit.Assert.Equal(expected, _calculator.Pinatas);
        }
    }
}
