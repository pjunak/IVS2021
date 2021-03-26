using System;

namespace Calculator.Classes
{
	public class MathLibClass
	{
		public MathLibClass() { }

		public double Addition(double doubleNumber, double doubleNumberToAdd)
		{
			return doubleNumber + doubleNumberToAdd;
		}

		public double Subtraction(double doubleNumber, double doubleNumberToSub)
		{
			return doubleNumber - doubleNumberToSub;
		}

		public double Multiplication(double doubleNumber, double doubleNumberToSub)
		{
			return doubleNumber * doubleNumberToSub;
		}

		public double Division(double doubleNumber, double doubleNumberToSub)
		{
			if (doubleNumberToSub == 0) return Double.NaN;
			else return doubleNumber / doubleNumberToSub;
		}

		public double Factorial(double doubleNumberForFactorial)
		{
			if (IsDoubleNumberNegative(doubleNumberForFactorial) || IsValueDouble(doubleNumberForFactorial)) return Double.NaN;

			int ToFact = (int)doubleNumberForFactorial;
			int Fact = 1;
			for (var i = 1; i <= ToFact; i++)
			{
				Fact *= i;
			}
			return Fact;
		}

		public double Power(double doubleNumber, double doubleNumberPower)
		{
			if (doubleNumberPower == 0 || IsDoubleNumberNegative(doubleNumberPower) || IsValueDouble(doubleNumberPower)) return double.NaN;
			return Math.Pow(doubleNumber, (int)doubleNumberPower);
		}

		public double NthRoot(double DoubleNumber, double DoubleNumberNthRoot)
		{
			if (IsDoubleNumberNegative(DoubleNumberNthRoot) ||
				IsDoubleNumberNegative(DoubleNumber) ||
				IsValueDouble(DoubleNumberNthRoot) ||
				(DoubleNumberNthRoot == 0)) return double.NaN;

			return Math.Pow(DoubleNumber, 1.0 / DoubleNumberNthRoot);
		}
		public double Sin(double DoubleNumber)
		{
			return Math.Sin(DoubleNumber);
		}

		private bool IsDoubleNumberNegative(double DoubleNumber) => (DoubleNumber < 0);
		private bool IsValueDouble(double DoubleNumber) => (DoubleNumber % 1 != 0);
	}
}
