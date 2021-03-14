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
		public ObservableCollection<string> Vysledky { get; set; }

		public MainWindowClass()
		{
			Vstupy = new ObservableCollection<string>
			{
				"",
				"",
				"",
				""
			};

			Vysledky = new ObservableCollection<string>
			{
				"",
				"",
				"",
				""
			};
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