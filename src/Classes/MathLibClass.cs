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
		 * @brief Prázdny konstruktor
		 */
		public MathLibClass() { }

		/** 
		 * @brief Sčítanie dvoch čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToAdd Pravý operand
		 * @return Vráti sčítaní dvoch čísel
		 */
		public double Addition(double doubleNumber, double doubleNumberToAdd)
		{
			return doubleNumber + doubleNumberToAdd;
		}

		/** 
		 * @brief Odčítaní dvoch čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToSub Pravý operand
		 * @return Vráti odčítání dvoch čísel
		 */
		public double Subtraction(double doubleNumber, double doubleNumberToSub)
		{
			return doubleNumber - doubleNumberToSub;
		}

		/** 
		 * @brief Násobení dvoch čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToMul Pravý operand
		 * @return Vráti vynásobení dvoch čísel
		 */
		public double Multiplication(double doubleNumber, double doubleNumberToMul)
		{
			return Math.Round((doubleNumber * doubleNumberToMul), 10);
		}

		/** 
		 * @brief Delení dvoch čísel
		 * @param doubleNumber Levý operand
		 * @param doubleNumberToDiv Pravý operand
		 * @return Vráti delení dvoch čísel, v případe delení nulo vráti double.NaN
		 */
		public double Division(double doubleNumber, double doubleNumberToDiv)
		{
			if (doubleNumberToDiv == 0) return double.NaN;
			else return Math.Round((doubleNumber / doubleNumberToDiv), 10);
		}

		/** 
		 * @brief Vypočítá faktoriál zadaného čísla
		 * @param doubleNumberForFactorial Zadané číslo
		 * @return Vrátí faktoriál zadaného čísla. Jestli je zadané číslo záporné nebo není celé, vrátí double.NaN
		 */
		public double Factorial(double doubleNumberForFactorial)
		{
			if (IsDoubleNumberNegative(doubleNumberForFactorial) || IsValueDouble(doubleNumberForFactorial)) return double.NaN;

			int ToFact = (int)doubleNumberForFactorial;
			int Fact = 1;
			for (var i = 1; i <= ToFact; i++)
			{
				Fact *= i;
			}
			return Fact;
		}

		/** 
		* @brief Vypočítá mocninu
		* @param doubleNumber Mocněnec
		* @param doubleNumberPower Mocnitel
		* @return Vrátí mocninu. Jestli je mocnitel menší než 1 a mocněnec je záporný, vrátí double.NaN.
		*/
		public double Power(double doubleNumber, double doubleNumberPower)
		{
			if (doubleNumberPower < 1.0 && doubleNumber < 0.0) return double.NaN;
			return Math.Round(Math.Pow(doubleNumber, doubleNumberPower), 10);
		}

		/** 
		* @brief Vypočítá sinus
		* @param DoubleNumber Parameter pro funkci sinus
		* @return Vrátí funkční hodnotu funkce sinus
		*/
		public double Sin(double DoubleNumber)
		{
			return Math.Round(Math.Sin(DoubleNumber), 10);
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
