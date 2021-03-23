using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
	public class ComputeClass
	{
		private Stack<double> TokenStack { get; set; }
		private MathLibClass MathLib { get; set; }

		public ComputeClass()
		{
			TokenStack = new Stack<double>();
			MathLib = new MathLibClass();
		}

		public double Compute(List<Token> Input)
		{
			double RightOperand;
			double LeftOperand;
			double TemporallResult;

			foreach (Token token in Input)
			{
				if(DoComputation(token))
				{
					(RightOperand, LeftOperand) = AssingOperands(token);

					if (token.operation == '+')
					{
						TemporallResult = MathLib.Addition(LeftOperand, RightOperand);
					}
					else if (token.operation == '-')
					{
						TemporallResult = MathLib.Subtraction(LeftOperand, RightOperand);
					}
					else if (token.operation == '*')
					{
						TemporallResult = MathLib.Multiplication(LeftOperand, RightOperand);
					}
					else if (token.operation == '/')
					{
						TemporallResult = MathLib.Division(LeftOperand, RightOperand);
					}
					else if (token.operation == 'q') // SQRT
					{
						TemporallResult = MathLib.NthRoot(LeftOperand, RightOperand);
					}
					else if (token.operation == 'p') // POW
					{
						TemporallResult = MathLib.Power(LeftOperand, RightOperand);
					}
					else if (token.operation == '!') // Factorial
					{
						TemporallResult = MathLib.Factorial(RightOperand);
					}
					else // Sin
					{
						TemporallResult = MathLib.Sin(RightOperand);
					}

					if (TemporallResult == Double.NaN) return Double.NaN;

					TokenStack.Push(TemporallResult);
				}
			}

			return TokenStack.Pop();
		}

		private (double, double) AssingOperands(Token token)
		{
			if (token.type == TokenType.other) return (TokenStack.Pop(), 0);
			else return (TokenStack.Pop(), TokenStack.Pop());
		}

		private bool DoComputation(Token token)
		{
			if (token.type == TokenType.operand) TokenStack.Push(token.operand);
			return token.type != TokenType.operand;
		}
	}
}
