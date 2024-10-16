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
        private int _timerValue;
        public int TimerValue
        {
            get => _timerValue;
            set
            {
                if (_timerValue != value)
                {
                    _timerValue = value;
                    OnPropertyChanged(nameof(TimerValue));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
