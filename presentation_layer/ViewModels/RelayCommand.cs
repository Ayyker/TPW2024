using System;
using System.Windows.Input;

namespace presentation_layer.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _Execute_Action;
        private readonly Func<bool> _Can_Execute_Evaluator;
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _Execute_Action = execute ?? throw new ArgumentNullException(nameof(execute));
            _Can_Execute_Evaluator = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _Can_Execute_Evaluator?.Invoke() ?? true;

        public void Execute(object parameter) => _Execute_Action.Invoke();
    }
}
