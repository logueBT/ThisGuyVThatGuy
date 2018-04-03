// <copyright file="ImageDicToUrlConverter.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using ThisGuyVThatGuy.Models;
    using Xamarin.Forms;

    public class ImageDicToUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Dictionary<string, PlayerImage> image = value as Dictionary<string, PlayerImage>;
                string url = image["default"].Url;
                return url;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
