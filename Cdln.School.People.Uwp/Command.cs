using System;
using System.Windows.Input;
using Windows.UI.Xaml.Data;
using System.ComponentModel;

namespace Cdln.School.People.Uwp
{
    public class Command : ICommand
    {
        private readonly string TargetPropertyName;
        private readonly Action<object> ExecuteAction;
        private readonly Predicate<object> CanExecutePredicate;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute, Predicate<object> canExecute, object target, string targetPropertyName)
        {
            if (target is INotifyPropertyChanged notify) { notify.PropertyChanged += OnTargetPropertyChanged; }
            if (target is ICollectionView view)
            {
                view.CurrentChanged += (sender, e) => InvokeCanExecuteChanged();
                view.VectorChanged += (sender, e) => InvokeCanExecuteChanged();
            }
            TargetPropertyName = targetPropertyName;
            CanExecutePredicate = canExecute;
            ExecuteAction = execute;
        }

        private void InvokeCanExecuteChanged()
        {
            var args = new EventArgs();
            CanExecuteChanged?.Invoke(this, args);
        }

        private void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TargetPropertyName)
            { InvokeCanExecuteChanged(); }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecutePredicate?.Invoke(parameter) ?? false;
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
