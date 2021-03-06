﻿using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Easyfood_Xamarin.Converter
{
    // Konverter za blob slike
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            byte[] bytes = value as byte[];
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
