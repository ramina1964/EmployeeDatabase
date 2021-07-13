using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmployeeDatabase.Annotations;

namespace EmployeeDatabase
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler CanExecuteChanged;
    }
}
