using System;
using System.Collections.Generic;
using System.Globalization;
using CognitiveServices.Models.Ocr;
using Xamarin.Forms;

namespace CognitiveServices.Converters
{
    public class ListOfLinesToOneStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a list of strings to one string containing all strings 
        /// separated with a comma.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lines = value as List<Line>;

            var oneString = string.Empty;

            if (lines != null)
            {
                foreach (var line in lines)
                {
                    foreach (var word in line.Words)
                    {
                        oneString += word.Text + ", ";
                    }
                }
            }

            return oneString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
