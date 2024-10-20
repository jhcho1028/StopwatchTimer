using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StopwatchTimer
{
    class NumericUpDownControl : Control
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDownControl),
                new PropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDownControl), new PropertyMetadata(0));

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDownControl), new PropertyMetadata(100));

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public NumericUpDownControl()
        {
            IncreaseCommand = new RelayCommand(_ => Value++);
            DecreaseCommand = new RelayCommand(_ => Value--);
        }

    }
}