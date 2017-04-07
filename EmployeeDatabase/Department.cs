namespace EmployeeDatabase
{
	public class Department
	{
		public int Id { get; set; }
		public string DeptName => $"Dept {Id}";
	}
}
