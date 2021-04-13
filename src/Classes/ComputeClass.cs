/*
Calculator, FITness StudIO 21
Copyright (C)

ComputeClass.cs: processing an input expression.
Full project can be found here: https://github.com/pjunak/IVS2021/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see https://www.gnu.org/licenses/.
Also add information on how to contact you by electronic and paper mail. 
 */

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
			if (Input == null) return Double.NaN;

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
