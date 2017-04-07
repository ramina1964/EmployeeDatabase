namespace EmployeeDatabase
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			var vm = new MainViewModel();
			DataContext = vm;
		}
	}
}
