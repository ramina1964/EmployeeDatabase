using System;
using System.ComponentModel;
using System.Windows;

namespace EmployeeDatabase
{
	public static class ViewModelLocator
	{
		public static bool GetAutoWireViewModel(DependencyObject obj)
		{ return (bool)obj.GetValue(GetAutoWireViewModelProperty); }

		public static void SetAutoWireViewModel(DependencyObject obj, bool value)
		{ obj.SetValue(GetAutoWireViewModelProperty, value); }

		public static readonly DependencyProperty GetAutoWireViewModelProperty =
			DependencyProperty.RegisterAttached("AutoWireViewModel",
			typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

		private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (DesignerProperties.GetIsInDesignMode(d))
			{ return; }

			var viewType = d.GetType();
			var viewTypeName = viewType.FullName;
			var viewModelTypeName = viewTypeName + "Model";
			var viewModelType = Type.GetType(viewModelTypeName);
			var viewModel = Activator.CreateInstance(viewModelType);

			((FrameworkElement)d).DataContext = viewModel;
		}
	}
}
