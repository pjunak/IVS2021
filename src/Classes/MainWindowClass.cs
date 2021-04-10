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

		private string Check(string Value)
		{
			/*if (CheckAfterFinal)
			{
				CheckAfterFinal = false;
				if ((Value.Length > Input.Length) && char.IsDigit(Value[Value.Length - 1]))
				{
					if (string.Equals(Value.Substring(0, Value.Length - 1) , Input))
					{
						Value = Value[Value.Length - 1].ToString();
					}
				}
			}*/

			string SyntaxCheckResult = SyntaxCheck.SyntaxCheck(Value, false);
			if (SyntaxCheckResult == null)
            {
				Error = "red";
            }
			else
            {
				Error = "black";
            }
			return Value;
		}

		//Takto sa definuje volanie funkcie, tato premena je naviazana na tlacidko '='
		public ICommand Calculate { get; set; }
		public ICommand BackInHistory { get; set; }
		public ICommand ForwardInHistory { get; set; }

		public static int IndexOfResultsInputs {get; set;}
		public static int Shifts { get; set; }
		public ObservableCollection<string> Inputs { get; set; }
		public ObservableCollection<string> Results { get; set; }
		public ToPostfixClass ToPostfix { get; set; }
		public ComputeClass ComputeResult { get; set; }
		public ToTokens ToToken { get; set; }
		public SyntaxClass SyntaxCheck { get; set; }

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

		public void Compute()
		{
			string SyntaxCheckResult = SyntaxCheck.SyntaxCheck(Input, true);
			if (SyntaxCheckResult == null)
            {
				Error = "red";
				return;
            }

			var Result = Math.Round(ComputeResult.Compute(ToPostfix.toPostfix(ToToken.toTokens(SyntaxCheckResult))), 10);
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

		public void BackInHistoryMethod()
		{
			int NumberOfCountedResults = Inputs.Count(s => s != "");
			if (NumberOfCountedResults > 0)
			{ 
				Shifts += 1;
				Input = Inputs[Math.Abs(IndexOfResultsInputs + Shifts -1) % NumberOfCountedResults];
			}
		}
		public void ForwardInHistoryMethod()
		{
			int NumberOfCountedResults = Inputs.Count(s => s != "");
			if (NumberOfCountedResults > 0)
			{
				Shifts -= 1;
				Input = Inputs[Math.Abs(IndexOfResultsInputs + Shifts - 1) % NumberOfCountedResults];
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
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