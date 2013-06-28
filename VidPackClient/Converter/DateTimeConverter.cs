﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace VidPackClient.Converter
{
    class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string returnValue = String.Empty;
            if (value != null)
            {
                returnValue = (string)value; 
            }

            return returnValue; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    namespace VidPackClient.Common
    {
        /// <summary>
        /// Value converter that translates true to false and vice versa.
        /// </summary>
        public sealed class BooleanNegationConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                return !(value is bool && (bool)value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                return !(value is bool && (bool)value);
            }
        }
    }

}
