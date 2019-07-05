using Panacea.Modules.RoomControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Panacea.Modules.RoomControl.Controls
{
    /// <summary>
    /// Interaction logic for SliderControl.xaml
    /// </summary>
    public partial class SliderControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SliderBackgroundProperty = DependencyProperty.Register(
            "SliderBackground", typeof(Brush), typeof(SliderControl), new PropertyMetadata(default(Brush)));

        public Brush SliderBackground
        {
            get { return (Brush)GetValue(SliderBackgroundProperty); }
            set { SetValue(SliderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty WritableProperty = DependencyProperty.Register(
            "Writable", typeof(bool), typeof(SliderControl), new PropertyMetadata(default(bool)));

        public bool Writable
        {
            get { return (bool)GetValue(WritableProperty); }
            set { SetValue(WritableProperty, value); }
        }

        public static readonly DependencyProperty DeviceTypeProperty = DependencyProperty.Register(
            "DeviceType", typeof(RefType), typeof(SliderControl), new PropertyMetadata(default(RefType), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var sc = (SliderControl)dependencyObject;
            sc.OnPropertyChanged("IsOnOff");
            sc.OnPropertyChanged("IsSlider");
            sc.OnPropertyChanged("ShowLabel");

        }

        public RefType DeviceType
        {
            get { return (RefType)GetValue(DeviceTypeProperty); }
            set { SetValue(DeviceTypeProperty, value); }
        }

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            "Category", typeof(DeviceType), typeof(SliderControl), new PropertyMetadata(default(DeviceType), PropertyChangedCallback2));

        private static void PropertyChangedCallback2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var sc = (SliderControl)dependencyObject;
            sc.OnPropertyChanged("Add");
        }

        public DeviceType Category
        {
            get { return (DeviceType)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public Visibility IsOnOff
        {
            get { return DeviceType == RefType.OnOff ? Visibility.Visible : Visibility.Collapsed; }
        }


        public Visibility IsSlider
        {
            get { return DeviceType == RefType.Range ? Visibility.Visible : Visibility.Collapsed; }
        }

        public int Add
        {
            get { return Category == Models.DeviceType.Glass ? 1 : 0; }
        }

        public Visibility ShowLabel
        {
            get
            {
                return DeviceType == RefType.OnOff ? Visibility.Collapsed : Visibility.Visible;
            }

        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(String), typeof(SliderControl), new PropertyMetadata(default(String)));

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(SliderControl), new PropertyMetadata(default(double)));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(int), typeof(SliderControl), new PropertyMetadata(default(int)));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(int), typeof(SliderControl), new PropertyMetadata(default(int)));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(SliderControl), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty MeasurementUnitProperty = DependencyProperty.Register(
            "MeasurementUnit", typeof(string), typeof(SliderControl), new PropertyMetadata(default(string)));

        public string MeasurementUnit
        {
            get { return (string)GetValue(MeasurementUnitProperty); }
            set { SetValue(MeasurementUnitProperty, value); }
        }

        public SliderControl()
        {
            InitializeComponent();
        }

        public event EventHandler<int> ValueChanged;
        public event EventHandler<string> StringValueChanged;

        private void OnButton_OnClick(object sender, RoutedEventArgs e)
        {
            Slider.Value = Slider.Maximum;
            OnValueChanged((int)Slider.Value);
        }

        private void OffButton_OnClick(object sender, RoutedEventArgs e)
        {
            Slider.Value = Slider.Minimum;
            OnValueChanged((int)Slider.Value);
        }

        private void OnButton2_OnClick(object sender, RoutedEventArgs e)
        {
            OnStringValueChanged("on");
        }

        private void OffButton2_OnClick(object sender, RoutedEventArgs e)
        {
            OnStringValueChanged("off");
        }
        void OnStringValueChanged(string value)
        {
            StringValueChanged?.Invoke(this, value);
        }

        void OnValueChanged(int value)
        {
            var h = ValueChanged;
            if (h != null) h(this, value);
        }
        private void BedStandSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            OnValueChanged((int)Slider.Value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var h = PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(name));
        }
    }
}
