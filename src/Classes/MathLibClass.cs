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
			return doubleNumber * doubleNumberToMul;
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
			else return doubleNumber / doubleNumberToDiv;
		}

		/** 
		 * @brief Vypočíta faktoriál zadaného čísla
		 * @param doubleNumberForFactorial Zadané číslo
		 * @return Vráti faktoríal zadaného čísla. Jesli je zadané číslo záporne nebo není celé vráti double.NaN
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
		* @brief Vypočíta mocninu
		* @param doubleNumber Mocněnec
		* @param doubleNumberPower Mocnitel
		* @return Vráti mocninu. Jesli je Mocnitel menší než 1 a Mocněnec je záporny vráti double.NaN.
		*/
		public double Power(double doubleNumber, double doubleNumberPower)
		{
			if (doubleNumberPower < 1.0 && doubleNumber < 0.0) return double.NaN;
			return Math.Pow(doubleNumber, doubleNumberPower);
		}

		/** 
		* @brief Vypočíta sínus
		* @param DoubleNumber Parameter pro funkci sínus
		* @return Vráti funkčí hodnotu funkce sínus
		*/
		public double Sin(double DoubleNumber)
		{
			return Math.Sin(DoubleNumber);
		}

		/** 
		* @brief Metóda zistí, jestli je číslo záporné
		* @param DoubleNumber Zadané číslo
		* @return Vráti True jestli je číslo záporne, jinak False.
		*/
		private bool IsDoubleNumberNegative(double DoubleNumber) => (DoubleNumber < 0);
		/** 
		* @brief Metóda zistí, jestli je číslo desatiné
		* @param DoubleNumber Zadané číslo
		* @return Vráti True jestli je číslo desatiné, jinak False.
		*/
		private bool IsValueDouble(double DoubleNumber) => (DoubleNumber % 1 != 0);
	}
}
