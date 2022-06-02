using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XamarinApis.Converters
{
    public class ConverterFavoritoImages : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool isFavorite = (bool)value;
                if (isFavorite == true)
                {
                    return "favorite.png";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
