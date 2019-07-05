using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Modules.RoomControl
{
    public static class PopularModes
    {
        public static void AllOff(this List<FrameworkElement> elementList)
        {
            foreach (var element in elementList)
            {
                if (element.GetType() == typeof(Slider))
                    ((Slider)element).Value = 0;
                else if (element.GetType() == typeof(Button))
                {
                    if ((string)((Button)element).Content == "Off")
                        ((Button)element).Visibility = Visibility.Hidden;
                    if ((string)((Button)element).Content == "On")
                    {
                        ((Button)element).Visibility = Visibility.Visible;
                    }
                }
            }
        }
        public static void Away(this List<FrameworkElement> elementlist)
        {
            foreach (var element in elementlist)
            {
                if (element.GetType() == typeof(Slider))
                    ((Slider)element).Value = 0;
            }
        }
        public static void WinterDay(this Dictionary<string, List<FrameworkElement>> elementList)
        {
            foreach (KeyValuePair<string, List<FrameworkElement>> element in elementList)
            {
                if (element.Key == "Lights")
                {
                    foreach (var light in element.Value)
                    {
                        ((Slider)light).Value = 30;
                    }
                }
                else
                {
                    foreach (var shade in element.Value)
                    {
                        ((Slider)shade).Value = 100;
                    }
                }
            }

        }
        public static void WinterNight(this Dictionary<string, List<FrameworkElement>> elementList)
        {
            foreach (KeyValuePair<string, List<FrameworkElement>> element in elementList)
            {
                if (element.Key == "Lights")
                {
                    foreach (var light in element.Value)
                    {
                        ((Slider)light).Value = 90;
                    }
                }
                else
                {

                    foreach (var shade in element.Value)
                    {
                        ((Slider)shade).Value = 0;
                    }
                }
            }
        }
        public static void SummerDay(this Dictionary<string, List<FrameworkElement>> elementList)
        {
            foreach (KeyValuePair<string, List<FrameworkElement>> element in elementList)
            {
                if (element.Key == "Lights")
                {
                    foreach (var light in element.Value)
                    {
                        ((Slider)light).Value = 10;
                    }
                }
                else
                {
                    foreach (var shade in element.Value)
                    {
                        ((Slider)shade).Value = 90;
                    }
                }
            }
        }
        public static void SummerNight(this Dictionary<string, List<FrameworkElement>> elementList)
        {
            foreach (KeyValuePair<string, List<FrameworkElement>> element in elementList)
            {
                if (element.Key == "Lights")
                {
                    foreach (var light in element.Value)
                    {
                        ((Slider)light).Value = 80;
                    }
                }
                else
                {
                    foreach (var shade in element.Value)
                    {
                        ((Slider)shade).Value = 0;
                    }
                }
            }
        }
    }
}
