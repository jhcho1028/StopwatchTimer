using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StopwatchTimer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public MainViewModel()
        {
            StopwatchViewModel = new StopwatchViewModel();
            TimerViewModel = new TimerViewModel();

            StopwatchView.DataContext = StopwatchViewModel;
            TimerView.DataContext = TimerViewModel;

            // 기본적으로 스탑워치 화면을 표시
            CurrentView = StopwatchView;

            // 명령 초기화
            ShowStopwatchCommand = new RelayCommand(SwitchToStopwatch);
            ShowTimerCommand = new RelayCommand(SwitchToTimer);
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public StopwatchView StopwatchView = new StopwatchView();
        public TimerView TimerView = new TimerView();

        public StopwatchViewModel StopwatchViewModel { get; }
        public TimerViewModel TimerViewModel { get; }

        public ICommand ShowStopwatchCommand { get; }
        public ICommand ShowTimerCommand { get; }

        private void SwitchToStopwatch() => CurrentView = StopwatchView;
        private void SwitchToTimer() => CurrentView = TimerView;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
