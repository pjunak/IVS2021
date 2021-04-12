using System;
using System.Collections.Generic;

namespace Calculator.Classes
{
	/**
	* @class ComputeClass
	* @brief Třída pro vypočítání výrazu z postfixové notace
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
		* @brief Metoda vypočítá výraz z infixové notace
		* @param Input List obsahující výraz v postfixové notaci
		* @return Vrátí vypočítaný výraz
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
					else if (token.operation == '^') // POW
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
			if (TokenStack.Count > 0)
            { // Ochrana před pádem z důvodu prázdného vstupu
				return TokenStack.Pop();
			}
			else
            {
				return 0;
            }
			
		}

		/**
		* @brief Metoda vybere ze zásobníku buď jedno čislo nebo dvě
		* @param token Token, který obsahuje buď operand nebo operátor
		* @return Vrátí hodnoty ze zásobníku.
		*/
		private (double, double) AssingOperands(Token token)
		{
			if (token.operation == '!' || token.operation == 's') return (TokenStack.Pop(), 0);
			else return (TokenStack.Pop(), TokenStack.Pop());
		}

		/**
		* @brief Metoda určí jestli se má vypočítat výraz na zásobníku
		* @param token Token, který obsahuje buď operand nebo operátor
		* @return Vrátí True jestli je na zásobníku operand, jinak False
		*/
		private bool DoComputation(Token token)
		{
			if (token.type == TokenType.operand) TokenStack.Push(token.operand);
			return token.type != TokenType.operand;
		}
	}
}
