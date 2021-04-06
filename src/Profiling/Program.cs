using Calculator.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Profiling
{
	/**
	* @class Profiling
	* @brief Třída pro vypočtení výběrové směrodatné odchylky
	*/
	class Profiling
	{
		static MathLibClass MathLib;
		/**
		* @brief Vstupní bod programu
		* @param Na vstupu očekáva soubor s posloupností čísel
		* @return na standartní výstup vypíše směrodatnú odchylku
		*/
		static void Main(string[] args)
		{
			MathLib = new MathLibClass();

			if (!File.Exists(args[0]))
			{
				Console.WriteLine("Subor neexistuje");
				System.Environment.Exit(0);
			}

			var InputNumbers = LoadFile(args);
			var Division = MathLib.Division(1, MathLib.Subtraction(InputNumbers.Count, 1));
			var NMeanXPower = MathLib.Multiplication(InputNumbers.Count, MathLib.Power(ComputeMean(InputNumbers), 2));

			var Sum = 0.0;
			foreach (var x in InputNumbers)
			{
				Sum += MathLib.Power(x, 2);
			}

			var InsideBrackets = MathLib.Subtraction(Sum, NMeanXPower);
			var S = MathLib.Power(MathLib.Multiplication(Division, InsideBrackets), 0.5);
			Console.WriteLine(S);
		}

		/**
		* @brief Metoda pro vypočítaní Aritmetického prumeru
		* @param List vstupních čísel
		* @return Vráti aritmetický prumer
		*/
		private static double ComputeMean(List<double> inputNumbers)
		{
			var Sum = 0.0;
			foreach (double Double in inputNumbers)
			{
				Sum = MathLib.Addition(Sum, Double);
			}

			return MathLib.Multiplication(MathLib.Division(1, inputNumbers.Count), Sum);
		}

		/**
		* @brief Metoda pro načtení čisel ze vstupního souboru
		* @param Cesta k souboru
		* @return Vráti List typu Double
		*/
		private static List<Double> LoadFile(string[] Input)
		{
			var Content = System.IO.File.ReadAllText(Input[0]);
			var Temporall = "";
			var ReturnList = new List<Double> { };

			foreach (var Character in Content)
			{
				if (Char.IsDigit(Character) || Character == '.' || Character == ',' || Character == '-')
				{
					if (Character == '.') Temporall += ',';
					else Temporall += Character;
				}
				else if (Temporall != "")
				{
					ReturnList.Add(Convert.ToDouble(Temporall));
					Temporall = "";
				}
			}

			if (Temporall != "")
			{
				ReturnList.Add(Convert.ToDouble(Temporall));
			}

			return ReturnList;
		}
	}
}
