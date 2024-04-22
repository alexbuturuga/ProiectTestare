using Proiect_DAW.Models;
using Xunit;
using XAssert = Xunit.Assert;

namespace Proiect_Daw.Tests;

public class PinataCalculatorTests
{
    private Account _calculator;

    public PinataCalculatorTests()
    {
        _calculator = new Account();
    }

    [Xunit.Theory]
    [InlineData(1, 1, 1, 1)] // Clasa 1 pentru amount și multiplier, Clasa 1 pentru bonus
    [InlineData(9, 1, 1, 9)] // Clasa 1 pentru amount și multiplier, Clasa 1 pentru bonus
    [InlineData(10, 1, 1, 11)] // Clasa 2 pentru amount și multiplier, Clasa 1 pentru bonus
    [InlineData(11, 1, 1, 12)] // Clasa 2 pentru amount și multiplier, Clasa 1 pentru bonus
    public void CalculatePinatas_AmountValidValues(int amount, int multiplier, int bonus, int expected)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(expected, _calculator.Pinatas);
    }

    [Xunit.Theory]
    [InlineData(10, 1, 1, 11)] // Valoare de frontieră pentru amount
    public void CalculatePinatas_AmountBoundaryValues(int amount, int multiplier, int bonus, int expected)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(expected, _calculator.Pinatas);
    }

    [Xunit.Theory]
    [InlineData(-1, 1, 1)] // Clasa 3 pentru amount
    [InlineData(-10, 1, 1)] // Clasa 3 pentru amount
    [InlineData(0, 1, 1)] // Valoare de frontieră pentru amount
    public void CalculatePinatas_AmountInvalidValues(int amount, int multiplier, int bonus)
    {
        // Act & Assert
        var exception = XAssert.Throws<ArgumentException>(() => _calculator.CalculatePinatas(amount, multiplier, bonus));
        XAssert.Equal("Amount and multiplier must be greater than zero.", exception.Message);

    }

    [Xunit.Theory]
    [InlineData(1, 1, 1, 1)] // Clasa 1 pentru multiplier
    [InlineData(9, 9, 1, 81)] // Clasa 1 pentru multiplier
    [InlineData(10, 10, 1, 101)] // Clasa 2 pentru multiplier
    [InlineData(11, 11, 1, 122)] // Clasa 2 pentru multiplier
    public void CalculatePinatas_MultiplierValidValues(int amount, int multiplier, int bonus, int expected)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(expected, _calculator.Pinatas);
    }

    [Xunit.Theory]
    [InlineData(1, 1, 1)] // Clasa 1 pentru bonus
    [InlineData(1, 1, -1)] // Clasa 2 pentru bonus
    [InlineData(1, 1, 0)] // Valoare de frontieră pentru bonus
    public void CalculatePinatas_BonusValidValues(int amount, int multiplier, int bonus)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(1, _calculator.Pinatas);
    }

    [Xunit.Theory]
    [InlineData(1, 0, 1)] // Valoare de frontieră pentru multiplier
    [InlineData(1, -1, 1)] // Valoare de frontieră pentru multiplier
    public void CalculatePinatas_MultiplierInvalidValues(int amount, int multiplier, int bonus)
    {
        // Act & Assert
        var exception = XAssert.Throws<ArgumentException>(() => _calculator.CalculatePinatas(amount, multiplier, bonus));
        XAssert.Equal("Amount and multiplier must be greater than zero.", exception.Message);

    }

    [Xunit.Theory]
    [InlineData(1, 1, int.MaxValue)] // Valoare de frontieră pentru bonus
    [InlineData(1, 1, int.MinValue)] // Valoare de frontieră pentru bonus
    public void CalculatePinatas_BonusBoundaryValues(int amount, int multiplier, int bonus)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(1, _calculator.Pinatas);
    }
}