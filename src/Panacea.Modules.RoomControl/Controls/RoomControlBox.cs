using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panacea.Modules.RoomControl.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UserPlugins.RoomControl.RoomControlBox"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UserPlugins.RoomControl.RoomControlBox;assembly=UserPlugins.RoomControl.RoomControlBox"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:RoomControlBox/>
    ///
    /// </summary>

    public class RoomControlBox : Control
    {
        Grid MainGrid;
        ContentControl Body;

        #region Properties

        public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.Register(
            "ImageUrl", typeof(string), typeof(RoomControlBox), new PropertyMetadata(default(string)));

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        /// <summary>
        /// Title Dependency Property
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(RoomControlBox));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// TitleBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(SolidColorBrush), typeof(RoomControlBox));
        public SolidColorBrush TitleBackground
        {
            get { return (SolidColorBrush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        /// <summary>
        /// BodyContent Dependency Property
        /// </summary>
        public static readonly DependencyProperty BodyContentProperty =
            DependencyProperty.Register("BodyContent", typeof(FrameworkElement), typeof(RoomControlBox));
        public FrameworkElement BodyContent
        {
            get { return (FrameworkElement)GetValue(BodyContentProperty); }
            set { SetValue(BodyContentProperty, value); }
        }
        #endregion

        #region Constructor & Initialization
        static RoomControlBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoomControlBox), new FrameworkPropertyMetadata(typeof(RoomControlBox)));
        }
        public RoomControlBox()
        {
            this.TitleBackground = new SolidColorBrush(Colors.White);
            this.Title = "";
        }
        #endregion

        #region Overrides
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MainGrid = this.Template.FindName("MainGrid", this) as Grid;
            Body = this.Template.FindName("Body", this) as ContentControl;
        }
        #endregion
    }
}
