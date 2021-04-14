/*
Calculator, FITness StudIO 21
Copyright (C)

MainWindowClass.cs: Data handler for the UI.
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calculator.Classes;
using System.Linq;

namespace Calculator.Classes
{
	/**
     * @class MainWindowClass
     * Hlavní bod aplikace, vstup do backend části.
     * @brief Hlavní třída, dědí z INotifyPropertyChanged, aby fungoval databinding.
     */
	public class MainWindowClass : INotifyPropertyChanged
	{
		public bool Final;
		private string _input;
		public string Input
		{
			get { return _input; }
			set
			{
				_input = Check(value);
			}
		}

		public bool CheckAfterFinal { get; set; }
		public string Error { get; set; }
		public string CorrectedValue { get; set; }

		public ICommand Calculate { get; set; }
		public ICommand BackInHistory { get; set; }
		public ICommand ForwardInHistory { get; set; }

		public static int IndexOfResultsInputs { get; set; }
		public static int Shifts { get; set; }
		public ObservableCollection<string> Inputs { get; set; }
		public ObservableCollection<string> Results { get; set; }
		public ToPostfixClass ToPostfix { get; set; }
		public ComputeClass ComputeResult { get; set; }
		public ToTokens ToToken { get; set; }
		public SyntaxClass SyntaxCheck { get; set; }

		/** 
		* @brief Konstruktor
		* Inicializuje proměnné
		*/
		public MainWindowClass()
		{
			ToPostfix = new ToPostfixClass();
			ComputeResult = new ComputeClass();
			ToToken = new ToTokens();
			SyntaxCheck = new SyntaxClass();

			IndexOfResultsInputs = 0;
			Shifts = 0;
			Inputs = new ObservableCollection<string> { "", "", "", "" };
			Results = new ObservableCollection<string> { "", "", "", "" };

			Calculate = new RelayCommand(Compute);
			BackInHistory = new RelayCommand(BackInHistoryMethod);
			ForwardInHistory = new RelayCommand(ForwardInHistoryMethod);

			Error = "black";
			Final = false;
			CheckAfterFinal = false;
		}

		/** 
		* @brief Metoda pro výpočet výrazu
		* Metoda zavolá metody objektů, které vrátí vyčíslený výraz. V případě chyby informuje View.
		*/
		public void Compute()
		{
			string SyntaxCheckResult = SyntaxCheck.SyntaxCheck(Input, true, this);
			if (SyntaxCheckResult == null)
			{
				Error = "red";
				return;
			}

			var tokens = ToToken.toTokens(SyntaxCheckResult);
			var postfix = ToPostfix.toPostfix(tokens);
			var Result = Math.Round(ComputeResult.Compute(postfix), 10);


			if (Double.IsNaN(Result))
			{
				Error = "red";
			}
			else
			{
				Inputs.Insert(0, SyntaxCheckResult);
				Results.Insert(0, Result.ToString());
				if (Inputs.Count > 4)
				{
					Inputs.RemoveAt(4);
					Results.RemoveAt(4);
				}
				/*
				Následující řádek byl vytvořen na základě odpovědí na dotaz na webu https://stackoverflow.com/questions/1546113/double-to-string-conversion-without-scientific-notation
				Double to string conversion without scientific notation. Stackoverflow [online]. 2019 [cit. 2021-04-10]. Dostupné z: https://stackoverflow.com/questions/1546113/double-to-string-conversion-without-scientific-notation
				*/
				Final = true;
				Input = Result.ToString("0." + new string('#', 339)); //Konvertuje na string beze ztrát a zachová decimální tvar
				CheckAfterFinal = true;
				Final = false;
				Shifts = 0;
			}
		}

		/** 
		* @brief Metoda pro procházení historie výrazů.
		* 
		* Metoda pro procházení historie výrazů. Umožňuje jít v historii dopředu.
		*/
		public void BackInHistoryMethod()
		{
			int NumberOfCountedResults = Inputs.Count(s => s != "");
			if (NumberOfCountedResults > 0)
			{
				Shifts += 1;
				Input = Inputs[Math.Abs(IndexOfResultsInputs + Shifts - 1) % NumberOfCountedResults];
			}
		}

		/** 
		* @brief Metoda pro procházení historie výrazů.
		* Metoda pro procházení historie výrazů. Umožňuje jít v historii dozadu.
		*/
		public void ForwardInHistoryMethod()
		{
			int NumberOfCountedResults = Inputs.Count(s => s != "");
			if (NumberOfCountedResults > 0)
			{
				Shifts -= 1;
				Input = Inputs[Math.Abs(IndexOfResultsInputs + Shifts - 1) % NumberOfCountedResults];
			}
		}

		/** 
         * @brief Metoda, která po každém vstupu zavolá syntaktickou analýzu
         * 
         * @param Value Vstupní řetězec.
         *
         * @return Vrátí upravený řetězec. V případě chyby informuje View.
         */
		private string Check(string Value)
		{
			CorrectedValue = Value;
			string SyntaxCheckResult = SyntaxCheck.SyntaxCheck(Value, false, this);
			if (SyntaxCheckResult == null)
			{
				Error = "red";
				return CorrectedValue;
			}
			else
			{
				Error = "black";
				return SyntaxCheckResult;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		/** 
		* @brief Metoda pro Databinding.
		* Metoda upozorní, pokud byla proměnná změněna.
		*/
		public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}