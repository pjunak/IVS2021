using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
	/**
	* @class ComputeClass
	* @brief Třída pro vypočítaní výrazu z postfixové notace
	*/
	public class ComputeClass
	{
		private Stack<double> TokenStack { get; set; }
		private MathLibClass MathLib { get; set; }

		/**
		* @brief Konstruktor
		* 
		* Inicializuje TokenStack a MathLib
		* 
		*/
		public ComputeClass()
		{
			TokenStack = new Stack<double>();
			MathLib = new MathLibClass();
		}

		/**
		* @brief Metóda vypočí výraz z infixové notace
		* @param Input List obsahujíci výraz v postfixové notaci
		* @return Vráti vypočítaný výraz
		*/
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
					else if (token.operation == 'p') // POW
					{
						TemporallResult = MathLib.Power(LeftOperand, RightOperand);
					}
					else if (token.operation == '!') // Factorial
					{
						TemporallResult = MathLib.Factorial(RightOperand);
					}
					else
					{
						TemporallResult = MathLib.Sin(RightOperand);
					}

					if (TemporallResult == Double.NaN) return Double.NaN;

					TokenStack.Push(TemporallResult);
				}
			}

			return TokenStack.Pop();
		}

		/**
		* @brief Metóda vybere ze zásobníku buď jedno čislo nebo dve
		* @param token Token, který obsahuje buď operand nebo operátor
		* @return Vráti hodnoty ze zásobníku.
		*/
		private (double, double) AssingOperands(Token token)
		{
			if (token.type == TokenType.other) return (TokenStack.Pop(), 0);
			else return (TokenStack.Pop(), TokenStack.Pop());
		}

		/**
		* @brief Metóda urči jestli se má vypočítat výraz na zásobníku
		* @param token Token, který obsahuje buď operand nebo operátor
		* @return Vráti True jestli je na zásbníku operand, jinak False
		*/
		private bool DoComputation(Token token)
		{
			if (token.type == TokenType.operand) TokenStack.Push(token.operand);
			return token.type != TokenType.operand;
		}
	}
}
