using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    /// <summary>
    /// Реализация интерфейса ICommand
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private Action<object> _action;
        private Func<object, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;  }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public DelegateCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Возможность выполнения
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Выполнение
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}
