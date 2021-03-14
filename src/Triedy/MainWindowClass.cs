using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kalkulacka.Triedy
{
	public class MainWindowClass : INotifyPropertyChanged
	{
		private string _vstup;
		public string Vstup
		{
			get { return _vstup; }
			set
			{
				_vstup = value;
			}
		}

		public ObservableCollection<string> Vstupy { get; set; }
		public ObservableCollection<string> VstupyPostFix { get; set; }
		public ObservableCollection<string> Vysledky { get; set; }
		public ToPostfixClass ToPostfix { get; set; }

		public MainWindowClass()
		{
			ToPostfix = new ToPostfixClass();

			Vstupy = new ObservableCollection<string> { "", "", "", "" };
			VstupyPostFix = new ObservableCollection<string> { "", "", "", "" };
			Vysledky = new ObservableCollection<string> { "", "", "", "" };

			//Debug pro ToPostFix
			Vstupy[0] = "a+b*(c^d-e)^(f+g*h)-i";
			Console.WriteLine(ToPostfix.ToPostfix(Vstupy[0]));
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