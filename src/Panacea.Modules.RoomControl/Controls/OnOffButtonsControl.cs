using Panacea.Controls;
using Panacea.Modules.RoomControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Panacea.Modules.RoomControl.Controls
{
    public class OnOffButtonsControl : Control
    {

        static OnOffButtonsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OnOffButtonsControl), new FrameworkPropertyMetadata(typeof(OnOffButtonsControl)));
        }

        public OnOffButtonsControl()
        {
            OnClickCommand = new RelayCommand(args =>
            {
                OnValueChanged("on");
            });
            OffClickCommand = new RelayCommand(args =>
            {
                OnValueChanged("off");
            });
        }

        public event EventHandler<string> ValueChanged;

        void OnValueChanged(string value)
        {
            ValueChanged?.Invoke(this, value);
        }

        public ICommand OnClickCommand
        {
            get { return (ICommand)GetValue(OnClickCommandProperty); }
            set { SetValue(OnClickCommandProperty, value); }
        }

        public static readonly DependencyProperty OnClickCommandProperty =
            DependencyProperty.Register("OnClickCommand", typeof(ICommand), typeof(OnOffButtonsControl), new PropertyMetadata(null));


        public ICommand OffClickCommand
        {
            get { return (ICommand)GetValue(OffClickCommandProperty); }
            set { SetValue(OffClickCommandProperty, value); }
        }

        public static readonly DependencyProperty OffClickCommandProperty =
            DependencyProperty.Register("OffClickCommand", typeof(ICommand), typeof(OnOffButtonsControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(String), typeof(OnOffButtonsControl), new PropertyMetadata(default(String)));

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(OnOffButtonsControl), new PropertyMetadata(0.0));


        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(OnOffButtonsControl), new PropertyMetadata(0.0));


        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(OnOffButtonsControl), new PropertyMetadata(0.0));



        public static ICommand GetValueChangedCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ValueChangedCommandProperty);
        }

        public static void SetValueChangedCommand(DependencyObject d, string value)
        {
            d.SetValue(ValueChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ValueChangedCommandProperty =
            DependencyProperty.RegisterAttached("ValueChangedCommand", typeof(ICommand), typeof(OnOffButtonsControl), new PropertyMetadata(null, OnValueChangedCommandChanged));

        private static void OnValueChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as OnOffButtonsControl;
            if (b == null) return;
            b.ValueChanged -= ValueChangedEvent;
            if (e.NewValue != null)
                b.ValueChanged += ValueChangedEvent;
        }

        private static void ValueChangedEvent(object sender, string e)
        {
            var b = sender as OnOffButtonsControl;
            var d = (b.Tag as Device);
            GetValueChangedCommand(b)?.Execute(new object[] { b.Tag, e});
        }
    }
}
