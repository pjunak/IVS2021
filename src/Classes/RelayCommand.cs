/*
Calculator, FITness StudIO 21
Copyright (C)

RelayCommnad.cs: Event handler.
Full project can be found here: https://github.com/pjunak/IVS2021/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see https://www.gnu.org/licenses/.
Also add information on how to contact you by electronic and paper mail. 
 */

using System;
using System.Windows.Input;

namespace Calculator.Classes
{
	/**
     * @class RelayCommand
     * 
     * @brief Třída, která definuje rozhraní ICommand.
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
