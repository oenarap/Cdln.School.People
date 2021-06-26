using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace School.People.Uwp.Controls
{
    public sealed class DoubleToGridLengthGridLengthToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double doubleValue)
            {
                if (parameter is string stringParamater)
                {
                    switch (stringParamater)
                    {
                        case "0": return new GridLength(doubleValue, GridUnitType.Auto);
                        case "1": return new GridLength(doubleValue, GridUnitType.Pixel);
                        case "2": return new GridLength(doubleValue, GridUnitType.Star);
                    }
                }
                return new GridLength(doubleValue);
            }
            else if (value is GridLength) { return ConvertBack(value, targetType, parameter, language); }

            return default(GridLength);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is GridLength gridLengthValue) { return gridLengthValue.Value; }
            else if (value is double) { return Convert(value, targetType, parameter, language); }

            return 0d;
        }
    }

    // DUPLICATE found in CustomControls.Services namespace
    public sealed class ThicknessToDoubleDoubleToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double doubleValue)
            {
                if (parameter is string stringParamater)
                {
                    switch (stringParamater.ToLower())
                    {
                        case "left":
                            return new Thickness(doubleValue, 0, 0, 0);

                        case "top":
                            return new Thickness(0, doubleValue, 0, 0);

                        case "right":
                            return new Thickness(0, 0, doubleValue, 0);

                        case "bottom":
                            return new Thickness(0, 0, 0, doubleValue);
                    }
                }
                return new Thickness(doubleValue);
            }
            else if (value is Thickness) { return ConvertBack(value, targetType, parameter, language); }

            return default(Thickness);
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Thickness thicknessValue)
            {
                if (parameter is string stringParamater)
                {
                    switch (stringParamater.ToLower())
                    {
                        case "left":
                            return thicknessValue.Left;

                        case "top":
                            return thicknessValue.Top;

                        case "right":
                            return thicknessValue.Right;

                        case "bottom":
                            return thicknessValue.Bottom;
                    }

                    return thicknessValue.Left;
                }
            }
            else if (value is double) { return Convert(value, targetType, parameter, language); }

            return default(double);
        }
    }

    public class ContentToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null) { return Visibility.Visible; }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ContentToGridLenghtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null) { return new GridLength(1.0, GridUnitType.Star); }
            return new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
