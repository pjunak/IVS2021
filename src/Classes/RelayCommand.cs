using System;
using System.Windows.Input;

namespace Calculator.Classes
{
	/**
     * @class RelayCommand
     * 
     * @brief Třída, která definuje rozhraní ICommand
     */
	/*
		Táto třída byla převzatá z projektu do ITU. Rok 2020 autoři Patrik Haas, Pavel Bednář, Matúš Viščor. 
	*/
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _executeAction;
		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> execute)
		{
			this._executeAction = execute;
		}

		public RelayCommand(Action execute) : this(p => execute())
		{
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_executeAction.Invoke(parameter);
		}
	}
}
