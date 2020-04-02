using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using DailyMealPlaner.Service_Layer;

namespace DailyMealPlaner.Presentation_Layer
{
    public class IsSelectedToImageConverter : IValueConverter
    {
        private static Service service = new Service();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage result = null;
            if (value is bool && (bool)value)
            {
                result = new BitmapImage(
                        new Uri("pack://application:,,,/Resources/check.png", UriKind.RelativeOrAbsolute));
            }
            else if((value is bool && !(bool)value))
            {
                string name = (parameter as TextBlock).Text;
                if (service.GetCategoryByName(name) != null)
                {
                    try
                    {
                        result = new BitmapImage(
                                new Uri("pack://application:,,,/Resources/" + name + ".png", UriKind.RelativeOrAbsolute));
                    }
                    catch (IOException)
                    {
                        result = new BitmapImage(
                            new Uri("pack://application:,,,/Resources/lunch.png", UriKind.RelativeOrAbsolute));
                    }
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
