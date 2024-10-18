using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopwatchTimer
{
    public class TimerViewModel: INotifyPropertyChanged
    {
        private TimerModel _currentTimer;
        private TimeSpan _inputTime;
        public ObservableCollection<TimerModel> LapTimes { get; set; }

        public TimerViewModel()
        {
            LapTimes = new ObservableCollection<TimerModel>();
            StartCommand = new RelayCommand(StartTimer);
            PauseCommand = new RelayCommand(PauseTimer);
            ResumeCommand = new RelayCommand(ResumeTimer);
            ResetCommand = new RelayCommand(ResetTimer);
            DeleteAllCommand = new RelayCommand(DeleteAllTimers);
        }

        public TimeSpan InputTime
        {
            get => _inputTime;
            set
            {
                _inputTime = value;
                OnPropertyChanged(nameof(InputTime));
            }
        }

        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResumeCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand DeleteAllCommand { get; }

        private void StartTimer()
        {
            var timer = new TimerModel();
            timer.StartTimer(InputTime);
            LapTimes.Add(timer);
            _currentTimer = timer;
        }

        private void PauseTimer()
        {
            _currentTimer?.PauseTimer();
        }

        private void ResumeTimer()
        {
            _currentTimer?.ResumeTimer();
        }

        private void ResetTimer()
        {
            _currentTimer?.ResetTimer();
        }

        private void DeleteAllTimers()
        {
            LapTimes.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
