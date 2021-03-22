using Calculator.Classes;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTests
{
	/*public class MathLibTests
	{
		[Theory]
		[InlineData(1.1, 2.2, 3.3)]
		public void AdditionOfTwoNumbers(double FloatNumber, double FloatNumberToAdd, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			var result = MathLib.Addition(FloatNumber, FloatNumberToAdd);

			//Assert

			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, -1.1)]
		[InlineData(2.2, 1.1, 1.1)]
		public void SubtractionOfTwoNumbers(double FloatNumber, double FloatNumberToSub, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			var result = MathLib.Subtraction(FloatNumber, FloatNumberToSub);

			//Assert

			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, 2.42)]
		[InlineData(1.1, -2.2, -2.42)]
		[InlineData(-1.1, -2.2, 2.42)]
		public void MultiplicationOfTwoNumbers(double FloatNumber, double FloatNumberToMul, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			var result = MathLib.Multiplication(FloatNumber, FloatNumberToMul);

			//Assert

			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 2.2, 0.5)]
		[InlineData(-1.1, 2.2, -0.5)]
		[InlineData(-1.1, -2.2, 0.5)]
		public void DivisionOfTwoNumbers(double FloatNumber, double FloatNumberToDiv, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			var result = MathLib.Division(FloatNumber, FloatNumberToDiv);

			//Assert

			Assert.Equal(result, Expected, 2);
		}

		[Theory]
		[InlineData(1.1, 0)]
		[InlineData(-1.1, 0)]
		public void DivisionOfZero(double FloatNumber, double ZeroDivision)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act + Assert
			Assert.Raises<DivideByZeroException>(MathLib.Division(FloatNumber, ZeroDivision));
		}
	}*/

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