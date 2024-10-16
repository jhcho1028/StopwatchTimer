using System;
using System.Windows.Input;

namespace StopwatchTimer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        // 기본 생성자: 매개변수를 받지 않는 Action을 사용하는 경우
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : this(execute != null ? new Action<object>(_ => execute()) : null,
                   canExecute != null ? new Func<object, bool>(_ => canExecute()) : (Func<object, bool>)null)
        {
        }

        // 매개변수를 받는 Action을 사용하는 경우
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
