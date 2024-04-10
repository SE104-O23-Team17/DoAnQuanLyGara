using System.Windows.Input;

namespace QuanLyGara.ViewModels.Windows
{
    public class ViewModelCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExcuteAction;

        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExcuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExcuteAction)
        {
            _executeAction = executeAction;
            _canExcuteAction = canExcuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExcuteAction == null ? true : _canExcuteAction(parameter);
        }
        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
    public class ViewModelCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Predicate<T> _canExcuteAction;

        public ViewModelCommand(Action<T> executeAction)
        {
            _executeAction = executeAction;
            _canExcuteAction = null;
        }

        public ViewModelCommand(Action<T> executeAction, Predicate<T> canExcuteAction)
        {
            _executeAction = executeAction;
            _canExcuteAction = canExcuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExcuteAction == null ? true : _canExcuteAction((T)parameter);
        }
        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }
    }
}
