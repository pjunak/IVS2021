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
		[InlineData(2, 0, 1)]
		[InlineData(2, -5.1, 0.0291)]
		[InlineData(2, 5.1, 34.297)]
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
		[InlineData(16, 0.5, 4)]
		[InlineData(16.5, 0.5, 4.062)]
		[InlineData(16, 0.25, 2)]
		[InlineData(16, 0.238, 1.935)]
		[InlineData(16, -0.5, 0.25)]
		[InlineData(-16, 0.5, Double.NaN)]
		public void NthRoot(double DoubleNumber, double DoubleNumberNthRoot, double Expected)
		{
			//Arrange
			var MathLib = new MathLibClass();

			//Act
			double result = MathLib.Power(DoubleNumber, DoubleNumberNthRoot);

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
		//Infix=(15-10-5-10) Postfix(15 10 - 5 - 10 -) Expected=(-10)
		[Fact(DisplayName = "Basic Subtraction test")]
		public void ComputeFromPostFixNotation_TestNumber1()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = -10;
			var Input = new List<Token>
			{
				new Token {type = TokenType.operand, operand = 15},
				new Token {type = TokenType.operand, operand = 10},
				new Token {type = TokenType.plusMinus, operation = '-'},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.plusMinus, operation = '-'},
				new Token {type = TokenType.operand, operand = 10},
				new Token {type = TokenType.plusMinus, operation = '-'},
			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=(5+5+5+15) Postfix=(5 5 + 5 + 15 +) Expected(30)
		[Fact(DisplayName = "Basic Addition test")]
		public void ComputeFromPostFixNotation_TestNumber2()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 30;
			var Input = new List<Token>
			{
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.plusMinus, operation = '+'},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.plusMinus, operation = '+'},
				new Token {type = TokenType.operand, operand = 15},
				new Token {type = TokenType.plusMinus, operation = '+'},
			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=(5+5*5), Postfix=(5 5 5 * +), Expected(30)
		[Fact(DisplayName = "Basic Multiplication test")]
		public void ComputeFromPostFixNotation_TestNumber3()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 30;
			var Input = new List<Token>
			{
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.mulDiv, operation = '*'},
				new Token {type = TokenType.plusMinus, operation = '+'},
			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=((2+3)*(7-2)/5), Postfix=(2 3 + 7 2 - * 5 /), Expected(5)
		[Fact(DisplayName = "Complex Multiplication and Division test")]
		public void ComputeFromPostFixNotation_TestNumber4()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 5;
			var Input = new List<Token>
			{
				new Token {type = TokenType.operand , operand = 2},
				new Token {type = TokenType.operand, operand = 3},
				new Token {type = TokenType.plusMinus, operation = '+'},
				new Token {type = TokenType.operand, operand = 7},
				new Token {type = TokenType.operand, operand = 2},
				new Token {type = TokenType.plusMinus, operation = '-'},
				new Token {type = TokenType.mulDiv, operation = '*'},
				new Token {type = TokenType.operand, operand = 5},
				new Token {type = TokenType.mulDiv, operation = '/'},
			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=(5*5 + 4^5*4), Postfix=(5 5 * 4 5 ^ 4 * +), Expected(4121)
		[Fact(DisplayName = "Basic Power test")]
		public void ComputeFromPostFixNotation_TestNumber6()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 4121;
			var Input = new List<Token>
			{
				new Token { type = TokenType.operand, operand = 5},
				new Token { type = TokenType.operand, operand = 5},
				new Token { type = TokenType.mulDiv, operation = '*'},
				new Token { type = TokenType.operand, operand = 4},
				new Token { type = TokenType.operand, operand = 5},
				new Token { type = TokenType.powerSquareFactor, operation = 'p'},
				new Token { type = TokenType.operand, operand = 4},
				new Token { type = TokenType.mulDiv, operation = '*'},
				new Token { type = TokenType.plusMinus, operation = '+'},

			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=(5.5/1.1+sqrt(16, 4)), Postfix=(5.5 1.1 / 16 4 sqrt +), Expected(9)
		[Fact(DisplayName = "Basic sqrt test")]
		public void ComputeFromPostFixNotation_TestNumber7()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 7;
			var Input = new List<Token>
			{
				new Token { type = TokenType.operand, operand = 5.5},
				new Token { type = TokenType.operand, operand = 1.1},
				new Token { type = TokenType.mulDiv, operation = '/'},
				new Token { type = TokenType.operand, operand = 16},
				new Token { type = TokenType.operand, operand = 4},
				new Token { type = TokenType.powerSquareFactor, operation = 'q'},
				new Token { type = TokenType.plusMinus, operation = '+'},

			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//ComputeFromPostFix Infix=(5.6+5.2*(120-5!)), Postfix=(5.6 5.2 120 5 ! - * +), Expected(5.6)
		[Fact(DisplayName = "Basic Factorial test")]
		public void ComputeFromPostFixNotation_TestNumber8()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 5.6;
			var Input = new List<Token>
			{
				new Token { type = TokenType.operand, operand = 5.6},
				new Token { type = TokenType.operand, operand = 5.2},
				new Token { type = TokenType.operand, operand = 120},
				new Token { type = TokenType.operand, operand = 5},
				new Token { type = TokenType.powerSquareFactor, operation = '!'},
				new Token { type = TokenType.plusMinus, operation = '-'},
				new Token { type = TokenType.mulDiv, operation = '*'},
				new Token { type = TokenType.plusMinus, operation = '+'},

			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 2);
		}

		//Infix=(5.5*5.5/5.5+sin(1)*sin(2)), Postfix=(5.5 5.5 * 5.5 / 1 sin 2 sin * +), Expected(6.265)
		[Fact(DisplayName = "Basic Sin test")]
		public void ComputeFromPostFixNotation_TestNumber9()
		{
			//Arrange
			var Compute = new ComputeClass();
			double Expected = 6.265;
			var Input = new List<Token>
			{
				new Token { type = TokenType.operand, operand = 5.5},
				new Token { type = TokenType.operand, operand = 5.5},
				new Token { type = TokenType.mulDiv, operation = '*'},
				new Token { type = TokenType.operand, operand = 5.5},
				new Token { type = TokenType.mulDiv, operation = '/'},
				new Token { type = TokenType.operand, operand = 1},
				new Token { type = TokenType.function, operation = 's'},
				new Token { type = TokenType.operand, operand = 2},
				new Token { type = TokenType.function, operation = 's'},
				new Token { type = TokenType.mulDiv, operation = '*'},
				new Token { type = TokenType.plusMinus, operation = '+'},

			};

			//Act
			double result = Compute.Compute(Input);

			//Assert
			Assert.Equal(result, Expected, 3);
		}
	}
}