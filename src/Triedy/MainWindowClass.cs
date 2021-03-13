using System;
using System.Collections.Generic;

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

	public List<string> Vstupy { get; set;}
	public List<string> Vysledky {get; set;}

	public MainWindowClass()
	{
		for (int i = 0; i < 4; i++)
		{
			Vysledky.Add("");
			Vstupy.Add("");
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
