using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeDatabase.Annotations;

namespace EmployeeDatabase
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			_random = new Random();
			Employees = LoadData();
			SelectedEmployee = Employees[0];
		}

		/***************************************** Class Interface *****************************************/
		public ObservableCollection<Person> Employees
		{
			get { return _employees; }

			set
			{
				_employees = value;
				//OnPropertyChanged();
			}
		}

		public Person SelectedEmployee
		{
			get { return _selectedEmployee; }
			set
			{
				_selectedEmployee = value;
				OnPropertyChanged(nameof(SelectedEmployee));

			}
		}

		/************************************ Private Fields and Methods ***********************************/
		private ObservableCollection<Person> LoadData()
		{
			var temp = Enumerable.Range(1, 100).Select(item => new Person()
			{
				Id = item,
				Name = $"Person {item}",
				Birthday = GenerateBirthDay().ToShortDateString(),
				Address = GenerateAddress(),
				Department = _random.Next(1, 4)
			}).Where(p => p.Id % 2 == 0).ToList();

			return new ObservableCollection<Person>(temp);
		}

		private string GenerateAddress()
		{ return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 21); }

		private DateTime GenerateBirthDay()
		{
			var startDate = new DateTime(1950, 1, 1);
			var endDate = new DateTime(1997, 1, 1);
			var range = (endDate - startDate).Days;
			var randomDay = _random.Next(0, range);
			var randomBirthday = startDate.AddDays(randomDay);
			return randomBirthday;
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private readonly Random _random;
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<Person> _employees;
		private Person _selectedEmployee;
	}
}
