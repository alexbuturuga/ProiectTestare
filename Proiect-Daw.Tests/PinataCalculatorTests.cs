using System.Runtime.InteropServices;
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

    // TESTE FUNCTIONALE
    
    // Partitionare in clase de echivalenta
    
    // Clasa 1 de echivalenta (toti parametrii) - valori intregi intre 1-9
    [Xunit.Theory]
    [InlineData(5, 5, 5, 25)]
    public void CalculatePinatas_AllParametersClass1(int amount, int multiplier, int bonus, int expected)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(expected, _calculator.Pinatas);
    }

    // Clasa 2 de echivalenta (toti parametrii) - valori pozitive intregi mai mari ca 10
    [Xunit.Theory]
    [InlineData(11, 11, 11, 132)]
    public void CalculatePinatas_AllParametersClass2(int amount, int multiplier, int bonus, int expected)
    {
        // Act
        _calculator.CalculatePinatas(amount, multiplier, bonus);

        // Assert
        XAssert.Equal(expected, _calculator.Pinatas);
    }
    
    // Clasa 3 de echivalenta (toti parametrii) - valori negative intregi
    [Xunit.Theory]
    [InlineData(-1, -1, -1)]
    public void CalculatePinatas_AllParametersClass3(int amount, int multiplier, int bonus)
    {
        // Act & Assert
        var exception = XAssert.Throws<ArgumentException>(() => _calculator.CalculatePinatas(amount, multiplier, bonus));
        XAssert.Equal("Amount and multiplier must be greater than zero.", exception.Message);
    }
    
    // Analiza valorilor de frontiera
    
    // Valori de frontiera (toti parametrii)
    [Xunit.Theory]
    [InlineData(1, 1, 1, 1)]
    [InlineData(9, 9, 9, 81)]
    [InlineData(10, 10, 10, 110)]
    [InlineData(-1, -1, -1, 0)]
    [InlineData(0, 0, 0, 0)]
    public void CalculatePinatas_BoundaryValues(int amount, int multiplier, int bonus, int expected)
    {
        // Act & Assert
        if (amount <= 0 || multiplier <= 0)
        {
            XAssert.Throws<ArgumentException>(() => _calculator.CalculatePinatas(amount, multiplier, bonus));
        }
        else
        {
            _calculator.CalculatePinatas(amount, multiplier, bonus);
            XAssert.Equal(expected, _calculator.Pinatas);
        }
    }
    
    [Xunit.Fact]
    public void CalculatePinatas_StatementCoverage()
    {
        _calculator.CalculatePinatas(10, 2, 5);
        XAssert.Equal(25, _calculator.Pinatas);
    }

    [Xunit.Theory]
    [InlineData(0, 5, 2, typeof(ArgumentException))] // Test for invalid amount
    [InlineData(20, 5, 2, 104)] // Test for tenth pinata
    public void CalculatePinatas_DecisionCoverage(int amount, int multiplier, int bonus, object expected)
    {
        switch (expected)
        {
            case int expectedPinatas:
                _calculator.CalculatePinatas(amount, multiplier, bonus);
                XAssert.Equal(expectedPinatas, _calculator.Pinatas);
                break;
            case Type exceptionType:
                XAssert.Throws(exceptionType, () => _calculator.CalculatePinatas(amount, multiplier, bonus));
                break;
        }
    }
    
    [Xunit.Theory]
    [InlineData(0, 0, 1, typeof(ArgumentException))] // Test for invalid amount and multiplier
    [InlineData(0, 5, 2, typeof(ArgumentException))] // Test for invalid amount
    [InlineData(2, 0, 2, typeof(ArgumentException))] // Test for invalid multiplier
    [InlineData(2, 5, 2, 10)] // Test for valid amount and multiplier
    public void CalculatePinatas_ConditionCoverage(int amount, int multiplier, int bonus, object expected)
    {
        switch (expected)
        {
            case int expectedPinatas:
                _calculator.CalculatePinatas(amount, multiplier, bonus);
                XAssert.Equal(expectedPinatas, _calculator.Pinatas);
                break;
            case Type exceptionType:
                XAssert.Throws(exceptionType, () => _calculator.CalculatePinatas(amount, multiplier, bonus));
                break;
        }
    }
}