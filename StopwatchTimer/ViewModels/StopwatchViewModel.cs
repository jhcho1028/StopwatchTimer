using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace StopwatchTimer
{
    public class StopwatchViewModel: INotifyPropertyChanged
    {
        private StopwatchModel _model;
        private bool _isRunning;
        private string _displayTime;

        public ICommand StartCommand { get; }
        public ICommand LapCommand { get; }

        public ObservableCollection<string> LapTimes { get; }

        public StopwatchViewModel()
        {
            _model = new StopwatchModel();
            _model.TimeUpdated += OnTimeUpdated;

            StartCommand = new RelayCommand(StartStop, CanStartStop);
            LapCommand = new RelayCommand(LapReset, CanLapReset);

            LapTimes = new ObservableCollection<string>();
            DisplayTime = "00:00:00";

            // 초기 버튼 텍스트 설정
            _isRunning = false;
            UpdateButtonLabels();
        }

        public string DisplayTime
        {
            get => _displayTime;
            private set
            {
                _displayTime = value;
                OnPropertyChanged(nameof(DisplayTime));
            }
        }

        private void OnTimeUpdated(TimeSpan elapsedTime)
        {
            DisplayTime = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void StartStop()
        {
            if (_isRunning)
            {
                _model.Stop();
            }
            else
            {
                _model.Start();
            }
            _isRunning = !_isRunning;
            UpdateButtonLabels();
        }

        private void LapReset()
        {
            if (_isRunning)
            {
                LapTimes.Add(_model.GetElapsedTime().ToString(@"hh\:mm\:ss"));
            }
            else
            {
                _model.Reset();
                LapTimes.Clear();
            }
        }

        private bool CanStartStop() => true;

        private bool CanLapReset() => true;

        private void UpdateButtonLabels()
        {
            // 버튼 텍스트 갱신을 알리기 위해 PropertyChanged 이벤트 발생
            OnPropertyChanged(nameof(StartButtonLabel));
            OnPropertyChanged(nameof(LapButtonLabel));
        }

        public string StartButtonLabel => _isRunning ? "Stop" : "Start";
        public string LapButtonLabel => _isRunning ? "Lap" : "Reset";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
