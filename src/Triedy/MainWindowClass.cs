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
	public List<string> VstupyPostFix { get; set; }
	public List<string> Vysledky {get; set;}

	public MainWindowClass()
	{
		for (int i = 0; i < 4; i++)
		{
			Vstupy.Add("");
			VstupyPostFix.Add("");
			Vysledky.Add("")
		}
	}

	Vstupy[0] = "2*5-10/5";
	Debug.WriteLine("{0}", Vstupy[0]);

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
