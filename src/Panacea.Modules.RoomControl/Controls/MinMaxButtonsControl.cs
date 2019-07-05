using Panacea.Controls;
using Panacea.Modules.RoomControl.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Panacea.Modules.RoomControl.Controls
{
    public class MinMaxButtonsControl : Control
    {
        static MinMaxButtonsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MinMaxButtonsControl), new FrameworkPropertyMetadata(typeof(MinMaxButtonsControl)));
        }

        public MinMaxButtonsControl()
        {
            MaxClickCommand = new RelayCommand(args =>
            {
                Value = MaxValue;
                OnValueChanged((int)Value);
            }, args => Value < MaxValue);
            UpClickCommand = new RelayCommand(args =>
            {
                if (Value + Step > MaxValue)
                {
                    Value = MaxValue;
                }
                else
                {
                    Value += Step;
                }
                OnValueChanged((int)Value);
            }, args => Value < MaxValue);
            DownClickCommand = new RelayCommand(args =>
            {
                if (Value - Step < MinValue)
                {
                    Value = MinValue;
                }
                else
                {
                    Value -= Step;
                }
                OnValueChanged((int)Value);
            }, args => Value > MinValue);
            MinClickCommand = new RelayCommand(args =>
            {
                Value = MinValue;
                OnValueChanged((int)Value);
            }, args => Value > MinValue);
        }



        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(MinMaxButtonsControl), new PropertyMetadata(0.0));



        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(MinMaxButtonsControl), new PropertyMetadata(0.0));




        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(MinMaxButtonsControl), new PropertyMetadata(0.0));


        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(MinMaxButtonsControl), new PropertyMetadata(0.0));




        public ICommand MaxClickCommand
        {
            get { return (ICommand)GetValue(MaxClickCommandProperty); }
            set { SetValue(MaxClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxClickCommandProperty =
            DependencyProperty.Register("MaxClickCommand", typeof(ICommand), typeof(MinMaxButtonsControl), new PropertyMetadata(null));




        public ICommand UpClickCommand
        {
            get { return (ICommand)GetValue(UpClickCommandProperty); }
            set { SetValue(UpClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpClickCommandProperty =
            DependencyProperty.Register("UpClickCommand", typeof(ICommand), typeof(MinMaxButtonsControl), new PropertyMetadata(null));




        public ICommand DownClickCommand
        {
            get { return (ICommand)GetValue(DownClickCommandProperty); }
            set { SetValue(DownClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DownClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DownClickCommandProperty =
            DependencyProperty.Register("DownClickCommand", typeof(ICommand), typeof(MinMaxButtonsControl), new PropertyMetadata(null));




        public ICommand MinClickCommand
        {
            get { return (ICommand)GetValue(MinClickCommandProperty); }
            set { SetValue(MinClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinClickCommandProperty =
            DependencyProperty.Register("MinClickCommand", typeof(ICommand), typeof(MinMaxButtonsControl), new PropertyMetadata(null));





        public RefType DeviceType
        {
            get { return (RefType)GetValue(DeviceTypeProperty); }
            set { SetValue(DeviceTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviceType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceTypeProperty =
            DependencyProperty.Register("DeviceType", typeof(RefType), typeof(MinMaxButtonsControl), new PropertyMetadata(RefType.OnOff, PropertyChangedCallback));


        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var sc = (MinMaxButtonsControl)dependencyObject;

            if (sc.DeviceType == RefType.Range)
            {
                sc.IsSlider = Visibility.Visible;
                sc.ShowValue = Visibility.Visible;
                sc.ShowLabel = Visibility.Collapsed;
            }
            else
            {
                sc.IsSlider = Visibility.Collapsed;
                sc.ShowValue = Visibility.Collapsed;
                sc.ShowLabel = Visibility.Visible;
            }
        }

        public Brush ButtonColor
        {
            get { return (Brush)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(MinMaxButtonsControl), new PropertyMetadata(default(Brush)));

        public Visibility IsSlider
        {
            get { return (Visibility)GetValue(IsSliderProperty); }
            set { SetValue(IsSliderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSlider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSliderProperty =
            DependencyProperty.Register("IsSlider", typeof(Visibility), typeof(MinMaxButtonsControl), new PropertyMetadata(Visibility.Collapsed));
        
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(MinMaxButtonsControl), new PropertyMetadata(null));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MinMaxButtonsControl), new PropertyMetadata(null));



        public string MeasurementUnit
        {
            get { return (string)GetValue(MeasurementUnitProperty); }
            set { SetValue(MeasurementUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MeasurementUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasurementUnitProperty =
            DependencyProperty.Register("MeasurementUnit", typeof(string), typeof(MinMaxButtonsControl), new PropertyMetadata(null));



        public Visibility ShowLabel
        {
            get { return (Visibility)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(Visibility), typeof(MinMaxButtonsControl), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ShowValue
        {
            get { return (Visibility)GetValue(ShowValueProperty); }
            set { SetValue(ShowValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register("ShowValue", typeof(Visibility), typeof(MinMaxButtonsControl), new PropertyMetadata(Visibility.Collapsed));

        public event EventHandler<int> ValueChanged;

        void OnValueChanged(int value)
        {
            ValueChanged?.Invoke(this,value);
        }

        public static ICommand GetValueChangedCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ValueChangedCommandProperty);
        }

        public static void SetValueChangedCommand(DependencyObject d, string value)
        {
            d.SetValue(ValueChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ValueChangedCommandProperty =
            DependencyProperty.RegisterAttached("ValueChangedCommand", typeof(ICommand), typeof(MinMaxButtonsControl), new PropertyMetadata(null, OnValueChangedCommandChanged));

        private static void OnValueChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as MinMaxButtonsControl;
            if (b == null) return;
            b.ValueChanged -= ValueChangedEvent;
            if (e.NewValue != null)
                b.ValueChanged += ValueChangedEvent;
        }

        private static void ValueChangedEvent(object sender, int e)
        {
            var b = sender as MinMaxButtonsControl;
            GetValueChangedCommand(b)?.Execute(b.Tag);
        }
    }
}
