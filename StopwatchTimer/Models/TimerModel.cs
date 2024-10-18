using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ComponentModel;

namespace StopwatchTimer
{
    public class TimerModel : INotifyPropertyChanged
    {
        private TimeSpan _remainingTime;
        private Timer _timer;
        private bool _isRunning;

        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }

        public TimerModel()
        {
            _timer = new Timer(1000); // 1초마다 실행
            _timer.Elapsed += OnTimerElapsed;
            _isRunning = false;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_remainingTime.TotalSeconds > 0)
            {
                RemainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                StopTimer();
            }
        }

        public void StartTimer(TimeSpan time)
        {
            RemainingTime = time;
            _timer.Start();
            _isRunning = true;
        }

        public void PauseTimer()
        {
            _timer.Stop();
            _isRunning = false;
        }

        public void ResumeTimer()
        {
            if (!_isRunning)
            {
                _timer.Start();
                _isRunning = true;
            }
        }

        public void ResetTimer()
        {
            PauseTimer();
            RemainingTime = TimeSpan.Zero;
        }

        private void StopTimer()
        {
            _timer.Stop();
            _isRunning = false;
            // 타이머 완료 알림 로직 추가 가능
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
