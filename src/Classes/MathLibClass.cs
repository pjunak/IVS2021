/*
Calculator, FITness StudIO 21
Copyright (C)

MathLibClass.cs: Math functions.
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

namespace Calculator.Classes
{
	/**
	* @class MathLibClass
	* @brief Třída pro matematické funkce
	*/
	public class MathLibClass
	{
		/** 
		 * @brief Prázdný konstruktor
		 */
		public MathLibClass() { }

		/** 
		 * @brief Sčítaní dvou čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToAdd Pravý operand
		 * @return Vrátí součet dvou čísel
		 */
		public double Addition(double doubleNumber, double doubleNumberToAdd)
		{
			return doubleNumber + doubleNumberToAdd;
		}

		/** 
		 * @brief Odčítaní dvou čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToSub Pravý operand
		 * @return Vrátí rozdíl dvou čísel
		 */
		public double Subtraction(double doubleNumber, double doubleNumberToSub)
		{
			return doubleNumber - doubleNumberToSub;
		}

		/** 
		 * @brief Násobení dvou čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToMul Pravý operand
		 * @return Vrátí výsledek vynásobení dvou čísel
		 */
		public double Multiplication(double doubleNumber, double doubleNumberToMul)
		{
			return (doubleNumber * doubleNumberToMul);
		}

		/** 
		 * @brief Dělení dvou čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToDiv Pravý operand
		 * @return Vrátí výsledek dělení dvou čísel, v případě dělení nulou vrátí double.NaN
		 */
		public double Division(double doubleNumber, double doubleNumberToDiv)
		{
			if (doubleNumberToDiv == 0) return double.NaN;
			else return (doubleNumber / doubleNumberToDiv);
		}

		/** 
		 * @brief Vypočítá faktoriál zadaného čísla
		 * @param doubleNumberForFactorial Zadané číslo
		 * @return Vrátí faktoriál zadaného čísla. Pokud je zadané číslo záporné nebo není celé, vrátí double.NaN
		 */
		public double Factorial(double doubleNumberForFactorial)
		{
			if (IsDoubleNumberNegative(doubleNumberForFactorial) || IsValueDouble(doubleNumberForFactorial)) return double.NaN;

			double ToFact = doubleNumberForFactorial;
			double Fact = 1;
			for (double i = 1; i <= ToFact; i++)
			{
				Fact *= i;
			}
			return Fact;
		}

		/** 
		* @brief Vypočítá mocninu
		* @param doubleNumber Mocněnec
		* @param doubleNumberPower Mocnitel
		* @return Vrátí mocninu. Pokud je mocnitel menší než 1 a mocněnec je záporný, vrátí double.NaN.
		*/
		public double Power(double doubleNumber, double doubleNumberPower)
		{
			if (doubleNumberPower < 1.0 && doubleNumber < 0.0) return double.NaN;
			return Math.Pow(doubleNumber, doubleNumberPower);
		}

		/** 
		* @brief Vypočítá sinus
		* @param DoubleNumber Parametr pro funkci sinus (v radiánech)
		* @return Vrátí funkční hodnotu funkce sinus
		*/
		public double Sin(double DoubleNumber)
		{
			return Math.Sin(DoubleNumber);
		}

		/** 
		* @brief Metoda zjistí, jestli je číslo záporné
		* @param DoubleNumber Zadané číslo
		* @return Vráti True jestli je číslo záporné, jinak False.
		*/
		private bool IsDoubleNumberNegative(double DoubleNumber) => (DoubleNumber < 0);
		/** 
		* @brief Metoda zjistí, jestli je číslo desetinné
		* @param DoubleNumber Zadané číslo
		* @return Vrátí True jestli je číslo desetinné, jinak False.
		*/
		private bool IsValueDouble(double DoubleNumber) => (DoubleNumber % 1 != 0);
	}
}
