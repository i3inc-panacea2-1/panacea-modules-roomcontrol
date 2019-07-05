using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ButtonsControl.xaml
    /// </summary>
    public partial class ButtonsControl : UserControl
    {
        public static readonly DependencyProperty MeasurementUnitProperty = DependencyProperty.Register(
            "MeasurementUnit", typeof(string), typeof(ButtonsControl), new PropertyMetadata(default(string)));

        public string MeasurementUnit
        {
            get { return (string)GetValue(MeasurementUnitProperty); }
            set { SetValue(MeasurementUnitProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(string), typeof(ButtonsControl), new PropertyMetadata(default(string)));
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ButtonsControl), new PropertyMetadata(default(string)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public ButtonsControl()
        {
            InitializeComponent();
        }
    }
}
