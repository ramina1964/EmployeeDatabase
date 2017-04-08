using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmployeeDatabase
{
	public class MainViewModel : ViewModelBase
	{
		public MainViewModel()
		{
			_random = new Random();
			LoadData();
			SelectedEmployee = Employees[0];
		}

		/***************************************** Class Interface *****************************************/
		public ObservableCollection<Person> Employees { get; set; }

		public ObservableCollection<Department> Departments { get; set; }

		public Person SelectedEmployee
		{
			get { return _selectedEmployee; }
			set
			{
				_selectedEmployee = value;
				OnPropertyChanged();
				SelectedDepartment = Departments.First(d => d.Id == _selectedEmployee.DepartmentId);
				OnPropertyChanged(nameof(SelectedDepartment));
			}
		}

		public Department SelectedDepartment { get; set; }

		/************************************ Private Fields and Methods ***********************************/
		private void LoadData()
		{
			var employees = Enumerable.Range(1, 100).Select(item => new Person()
			{
				Id = item,
				Name = $"Person {item}",
				Birthday = GenerateBirthDay().ToShortDateString(),
				Address = GenerateAddress(),
				DepartmentId = _random.Next(1, 4)
			}).Where(p => p.Id % 2 == 0).ToList();

			Employees = new ObservableCollection<Person>(employees);

			var depts = Enumerable.Range(1, 3).Select(d => new Department(d)).ToList();
			Departments = new ObservableCollection<Department>(depts);
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

		private readonly Random _random;
		private Person _selectedEmployee;
		//private ObservableCollection<Person> _employees;
		//private Department _selectedDepartment;
		//private ObservableCollection<Department> _departments;
	}
}
