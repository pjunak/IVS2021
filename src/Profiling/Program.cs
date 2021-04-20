using Calculator.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Profiling
{
	/**
	* @class Profiling
	* @brief Třída pro vypočtení výběrové směrodatné odchylky.
	*/
	class Profiling
	{
		static MathLibClass MathLib;
        /**
		* @brief Vstupní bod programu.
		* @param Na vstupu očekává soubor s posloupností čísel, nebo bude načítavat vstup ze standartního vstupu, dokud uživatel nezadá CTLR+D.
		* @return Na standardní výstup vypíše směrodatnou odchylku.
		*/
        static void Main(string[] args)
		{
			MathLib = new MathLibClass();
            List<double> InputNumbers;

            if (args.Length == 0 || !File.Exists(args[0]))
            {
                InputNumbers = LoadFile(LoadInput());
            }
            else
            {
                InputNumbers = LoadFile(args);
            }
            if (InputNumbers.Count == 0) Environment.Exit(0);

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
		* @brief Metoda pro vypočítání aritmetického průměru.
		* @param inputNumbers \c List vstupních čísel.
		* @return Vrátí aritmetický průměr.
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
		* @brief Metoda pro načtení čísel ze vstupního souboru nebo vlastního \c stringu.
		* @param Input Cesta k souboru / pole 2  \c stringov.
		* @return Vrátí \c List typu \c double.
		*/
		private static List<Double> LoadFile(string[] Input)
		{
            string Content;
            if (Input.Length == 2) Content = Input[0];
            else Content = System.IO.File.ReadAllText(Input[0]);

			var Temporall = "";
			var ReturnList = new List<Double> { };

			foreach (var Character in Content)
			{
				if (Char.IsDigit(Character) || Character == '.' || Character == ',' || Character == '-')
				{
					if (Character == ',') Temporall += '.';
					else Temporall += Character;
				}
				else if (Temporall != "")
				{
                    try
                    {
                        ReturnList.Add(Convert.ToDouble(Temporall, new CultureInfo("en-US")));
                    }
                    catch
                    {
                        continue;
                    }
					Temporall = "";
				}
			}

			if (Temporall != "")
			{
                try
                {
                    ReturnList.Add(Convert.ToDouble(Temporall, new CultureInfo("en-US")));
                }
                catch
                {
                }
			}

			return ReturnList;
		}

        /**
		* @brief Metoda načítá znakt ze z standartního vstupu.
		* @return Vrátí \c string, kde v prvním indexu je celkový vstup uživatele a ve druhém je indikátor pro metodu \c LoadFile, že uživatel zadával ručně znaky.
		*/
        private static string[] LoadInput()
        {
            int InputInt;
            string[] InputString = new string [2];

            while ((InputInt = Console.Read()) != 4)
            {
                InputString[0] += (char)InputInt;
            }
            InputString[1] = "";

            return InputString;
        }
    }
}