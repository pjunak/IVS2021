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
     * Je to hlavní bod aplikace, vstup do backend části.
     * @brief Hlavní třída, dedí z INotifyPropertyChanged, aby fungoval databinding.
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
		* Inicializuje promnené
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
		* @brief Metóda na výpoče výrazu
		* Metóda zavolá metódy objektú, které vráti vyčíslený výraz. V prípade chyby informuje View.
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
		* @brief Metóda pro přecházení histórie výrazú
		* Metóda umožní jít v história dopředu
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
		* @brief Metóda pro přecházení histórie výrazú
		* Metóda umožní jít v história dozadu
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
         * @brief Metóda, která po každém vstupu zavolá syntaktickou analýzu
         * 
         * @param Value Vstupní řetězec.
         *
         * @return Vráti upravený řetezec. V Prípade chyby informuje View.
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
		* @brief Metóda pro Databinding
		* Metóda upozorní, že byla promňená, byla zmenena
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