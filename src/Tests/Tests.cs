using Calculator.Classes;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTests
{
	public class MathLibTests
	{
		[Theory]
		[InlineData(1.1, 2.2, 3.3)]
		public void AdditionOfTwoNumbers(double DoubleNumber, double DoubleNumberToAdd, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Addition(DoubleNumber, DoubleNumberToAdd);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, -1.1)]
		[InlineData(2.2, 1.1, 1.1)]
		public void SubtractionOfTwoNumbers(double DoubleNumber, double DoubleNumberToSub, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Subtraction(DoubleNumber, DoubleNumberToSub);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, 2.42)]
		[InlineData(1.1, -2.2, -2.42)]
		[InlineData(-1.1, -2.2, 2.42)]
		public void MultiplicationOfTwoNumbers(double DoubleNumber, double DoubleNumberToMul, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Multiplication(DoubleNumber, DoubleNumberToMul);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, 0.5)]
		[InlineData(-1.1, 2.2, -0.5)]
		[InlineData(-1.1, -2.2, 0.5)]
		[InlineData(1.1, 0, double.NaN)]
		[InlineData(-1.1, 0, double.NaN)]
		public void DivisionOfTwoNumbers(double DoubleNumber, double DoubleNumberToDiv, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Division(DoubleNumber, DoubleNumberToDiv);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(5, 120)]
		[InlineData(0, 1)]
		[InlineData(1, 1)]
		[InlineData(5.1, Double.NaN)]
		[InlineData(-5, Double.NaN)]
		public void Factorial(double DoubleNumber, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Factorial(DoubleNumber);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(2, 5, 32)]
		[InlineData(2, 0, Double.NaN)]
		[InlineData(2, -5.1, Double.NaN)]
		[InlineData(2, 5.1, Double.NaN)]
		public void Power(double DoubleNumber, double DoubleNumberPower, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Power(DoubleNumber, DoubleNumberPower);

			//Assert
			Assert.Equal(result, Expected, 3);
		}

		[Theory]
		[InlineData(16, 2, 4)]
		[InlineData(16.5, 2, 4.062)]
		[InlineData(16, 4, 2)]
		[InlineData(16, 4.2, Double.NaN)]
		[InlineData(16, 0, Double.NaN)]
		[InlineData(16, -1, Double.NaN)]
		[InlineData(-16, 2, Double.NaN)]
		public void NthRoot(double DoubleNumber, double floatNumberNthRoot, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.NthRoot(DoubleNumber, floatNumberNthRoot);

			//Assert
			Assert.Equal(result, Expected, 3);
		}

		[Theory]
		[InlineData(2, 0.909)]
		[InlineData(0, 0)]
		[InlineData(-2, -0.909)]
		public void Sin(double DoubleNumber, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Sin(DoubleNumber);

			//Assert
			Assert.Equal(result, Expected, 3);
		}
	}

	public class ComputeClassTests
	{
		//Token Struct for testing
		public struct Token 
		{
			public string Type;
			public string Data;
		}

		public static IEnumerable<object[]> ComputeInputData()
		{
			//Simple Subtraction
			yield return new object[]
			{
				new object[]
				{
					new Token {Type = "Number", Data = "15"},
					new Token {Type = "Number", Data = "10"},
					new Token {Type = "Operator", Data = "-"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Operator", Data = "-"},
					new Token {Type = "Number", Data = "10"},
					new Token {Type = "Operator", Data = "-"},
				},
				-10
			};

			//Simple Addition
			yield return new object[]
			{
				new object[]
				{
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Operator", Data = "+"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Operator", Data = "+"},
					new Token {Type = "Number", Data = "15"},
					new Token {Type = "Operator", Data = "+"},
				},
				30
			};

			//Check simple multiplication
			yield return new object[]
			{
				new object[]
				{
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Operator", Data = "*"},
					new Token {Type = "Operator", Data = "+"},
				},
				30
			};

			//More Complex with Addition, Subtraction, Multiplication and Division
			yield return new object[]
			{
				new object[]
				{
					new Token {Type = "Number", Data = "2"},
					new Token {Type = "Number", Data = "3"},
					new Token {Type = "Operator", Data = "+"},
					new Token {Type = "Number", Data = "7"},
					new Token {Type = "Number", Data = "2"},
					new Token {Type = "Operator", Data = "-"},
					new Token {Type = "Operator", Data = "*"},
					new Token {Type = "Number", Data = "5"},
					new Token {Type = "Operator", Data = "/"},
				},
				5
			};
			//TODO factorial, pow, sqrt, sin
		}

		[Theory]
		[MemberData(nameof(ComputeInputData))]
		public void ComputeFromPostFixNotation_Simple_Substraction(object[] Input, double Expected)
		{
			//Arrange
			var Compute = new ComputeClass();

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}
	}
}