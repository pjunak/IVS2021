using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

		public ObservableCollection<string> Inputs { get; set; }
		public ObservableCollection<string> ResultsPostFix { get; set; }
		public ObservableCollection<string> Results { get; set; }
		public ToPostfixClass ToPostfix { get; set; }

		public MainWindowClass()
		{
			ToPostfix = new ToPostfixClass();

			Inputs = new ObservableCollection<string> { "", "", "", "" };
			ResultsPostFix = new ObservableCollection<string> { "", "", "", "" };
			Results = new ObservableCollection<string> { "", "", "", "" };

			//Debug pro ToPostFix
			Inputs[0] = "a+b*(c^d-e)^(f+g*h)-i";
			Console.WriteLine(ToPostfix.ToPostfix(Inputs[0]));
		}

		public void Compute()
		{
			//ToPostfix.ToPostfix(Vstup);
			//Vstupy[(Index % 4)] = ToPostfix.GetResult();
			//Compute.Compute(ToPostfix.GetResult());
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