using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calculator.Classes;

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
				_input = value;
			}
		}

		//Takto sa definuje volanie funkcie, tato premena je naviazana na tlacidko '='
		public ICommand Calculate { get; set; }

		public static int IndexOfResultsInputs {get; set;}
		public ObservableCollection<string> Inputs { get; set; }
		public ObservableCollection<string> Results { get; set; }
		public ToPostfixClass ToPostfix { get; set; }
		public ComputeClass ComputeResult { get; set; }

		public MainWindowClass()
		{
			ToPostfix = new ToPostfixClass();
			ComputeResult = new ComputeClass();

			/** TODO test toTokens a toPostfix
			 * 
			string str = "x^y/(5*z)+10!";
			List<Token> tokens = new List<Token>();
			tokens = ToTokens.toTokens(str);
			List <Token> result = new List<Token>();
			result = ToPostfixClass.toPostfix(tokens);

			result.ForEach(item => Console.Write(item));

			*/

			IndexOfResultsInputs = 0;
			Inputs = new ObservableCollection<string> { "", "", "", "" };
			Results = new ObservableCollection<string> { "", "", "", "" };

			Calculate = new RelayCommand(TestFunction);
		}

		public void Compute()
		{
			//ToPostfix.ToPostfix(Vstup);
			//Vstupy[(Index % 4)] = ToPostfix.GetResult();
			//Compute.Compute(ToPostfix.GetResult());
		}

		public void TestFunction()
		{
			Inputs[IndexOfResultsInputs] = Input;
			Results[IndexOfResultsInputs] = "5";

			IndexOfResultsInputs = (IndexOfResultsInputs + 1) % 4;
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