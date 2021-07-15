using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDatabase
{
    public class MainViewModel : ObservableObject
    {
        // Constructor
        public MainViewModel()
        {
            _random = new Random();
            LoadData();
            RunCommand = new RelayCommand(CanExecuteTask, ExecuteTask);

            SelectedEmployee = Employees[0];
        }

        /***************************************** Class Interface *****************************************/
        public ICommand RunCommand { get; set; }

        // Important for data binding
        public ObservableCollection<Person> Employees { get; set; }

        public ObservableCollection<Department> Departments { get; set; }

        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set => _selectedDepartment = value;
        }

        public Person SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                var isChanged = SetProperty(ref _selectedEmployee, value);

                // Also invoke property SelectedDepartment with the updated value.
                if (isChanged)
                {
                    var newDept = Departments.FirstOrDefault(d => d.Id == value.DepartmentId);
                    _ = SetProperty(ref _selectedDepartment, newDept, nameof(SelectedDepartment));
                }
            }
        }

        /************************************ Private Fields and Methods ***********************************/
        private void LoadData()
        {
            var employees = Enumerable.Range(1, 100).Select(no => new Person()
            {
                Id = no,
                Name = $"Person No. {no}",
                Birthday = GenerateBirthDay().ToShortDateString(),
                Address = GenerateAddress(),
                DepartmentId = _random.Next(1, 4)
            }).Where(p => p.Id % 2 == 0).ToList();
            Employees = new ObservableCollection<Person>(employees);

            var departments = Enumerable.Range(1, 3).Select(no => new Department(no)).ToList();
            Departments = new ObservableCollection<Department>(departments);
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

        private async void ExecuteTask(object parameter)
        {
            var id = (int)parameter;

            // This is a long running task and should be awaited:
            await LongRunningTask();

            SetNextEmployee(id);
        }

        private bool CanExecuteTask(object obj) => true;

        // Simulate a long running task by waiting 3 seconds.
        public Task LongRunningTask() => Task.Delay(3000);

        private void SetNextEmployee(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id > id);

            // Alternative to the approach above is extracting id directly from SelectedEmployee:
            // var employee = Employees.FirstOrDefault(e => e.Id > SelectedEmployee.Id);

            SelectedEmployee = employee ?? Employees.First();
        }

        private readonly Random _random;
        private Person _selectedEmployee;
        private Department _selectedDepartment;
    }
}
