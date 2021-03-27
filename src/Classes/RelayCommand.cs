using System;
using System.Windows.Input;

namespace Calculator.Classes
{
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
