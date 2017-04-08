﻿using System;
using System.Windows.Input;

namespace EmployeeDatabase
{
	public class RelayCommand : ICommand
	{
		public RelayCommand(Predicate<object> canExecute, Action<object> execute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		/***************************************** Private Fields ******************************************/
		//private Predicate<object> _canExecute;
		//private Action<object> _execute;
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;
	}
}
