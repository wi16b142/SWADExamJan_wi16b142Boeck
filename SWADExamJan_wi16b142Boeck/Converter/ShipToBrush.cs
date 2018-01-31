using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SWADExamJan_wi16b142Boeck.Converter
{
    public class ShipToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Container": return new SolidColorBrush(Colors.Red);
                case "Ferry": return new SolidColorBrush(Colors.Green);
                case "Cruiser": return new SolidColorBrush(Colors.Yellow);
                default:
                    break;
            }

            return new SolidColorBrush(Colors.Orange); //dummy, because compiler would complain that there is no "return value"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
