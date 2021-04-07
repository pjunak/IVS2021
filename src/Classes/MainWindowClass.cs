﻿using System;
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
				_input = check(value);
			}
		}

		private string check(string value)
		{
			//SyntaxCheck.SyntaxCheck(value, false);
			return value;
		}

		//Takto sa definuje volanie funkcie, tato premena je naviazana na tlacidko '='
		public ICommand Calculate { get; set; }

		public static int IndexOfResultsInputs {get; set;}
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
			Inputs = new ObservableCollection<string> { "", "", "", "" };
			Results = new ObservableCollection<string> { "", "", "", "" };

			Calculate = new RelayCommand(Compute);
		}

		public void Compute()
		{
			//if(SyntaxCheck.SyntaxCheck())
			var Result = ComputeResult.Compute(ToPostfix.toPostfix(ToToken.toTokens(Input)));

			Inputs[(IndexOfResultsInputs) % 4] = Input;
			Results[(IndexOfResultsInputs) % 4] = Result.ToString();
			IndexOfResultsInputs++;

			Input = Result.ToString();
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