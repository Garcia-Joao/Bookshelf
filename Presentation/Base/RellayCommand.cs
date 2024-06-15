using System.Windows.Input;

public class RelayCommand : ICommand {
    private readonly Action<object> _execute;
    private readonly Action _executeWithoutParameter;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null) {
        _execute = execute;
        _canExecute = canExecute;
    }

    public RelayCommand(Action executeWithoutParameter, Predicate<object> canExecute = null) {
        _executeWithoutParameter = executeWithoutParameter;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter) {
        if (_execute != null) {
            _execute(parameter);
        } else if (_executeWithoutParameter != null) {
            _executeWithoutParameter();
        }
    }

    public event EventHandler CanExecuteChanged {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
