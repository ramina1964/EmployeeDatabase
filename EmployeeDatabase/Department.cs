namespace EmployeeDatabase
{
	public class Department
	{
		public Department(int id)
		{
			Id = id;
			DeptName = ToString();
		}

		public int Id { get; set; }
		public string DeptName { get; set; }
		public sealed override string ToString()
		{
			return $"Dept {Id}";
		}
	}
}
