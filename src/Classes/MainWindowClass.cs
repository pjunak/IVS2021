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
		private string _input;
		public string Input
		{
			get { return _input; }
			set
			{
				_input = Check(value);
			}
		}

		public string Error { get; set; }

		private string Check(string Value)
		{
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
		}

		public void Compute()
		{
			string SyntaxCheckResult = SyntaxCheck.SyntaxCheck(Input, true);
			if (SyntaxCheckResult == null)
            {
				Error = "red";
				return;
            }

			var Result = Math.Round(ComputeResult.Compute(ToPostfix.toPostfix(ToToken.toTokens(Input))), 10);
			if (Double.IsNaN(Result))
			{
				Error = "red";
			}
			else
			{
				Inputs.Insert(0, Input);
				Results.Insert(0, Result.ToString());
				if (Inputs.Count > 4)
				{
					Inputs.RemoveAt(4);
					Results.RemoveAt(4);
				}
				Input = Result.ToString();
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