using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

namespace StopwatchTimer
{
    public class StopwatchModel
    {
        private DispatcherTimer _timer;
        private TimeSpan _elapsedTime;
        private DateTime _startTime;

        public event Action<TimeSpan> TimeUpdated;

        public StopwatchModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
            _elapsedTime = TimeSpan.Zero;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedTime = DateTime.Now - _startTime;
            TimeUpdated?.Invoke(_elapsedTime);
        }

        public void Start()
        {
            _startTime = DateTime.Now - _elapsedTime;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Reset()
        {
            _timer.Stop();
            _elapsedTime = TimeSpan.Zero;
            TimeUpdated?.Invoke(_elapsedTime);
        }

        public TimeSpan GetElapsedTime()
        {
            return _elapsedTime;
        }
    }
}
