using System;
using System.Linq;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using Cdln.School.People.Uwp.Models.Addresses;
using Windows.Globalization.DateTimeFormatting;

namespace Cdln.School.People.Uwp.Utils
{
    public class TextLenghtToRemainingCharactersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int remainder = 0;
            if (value is int textLenght && parameter is string strParam)
            {
                if (int.TryParse(strParam, out int maxLenght)) { remainder = maxLenght - textLenght; }
            }
            return $"{remainder} characters remaining.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjectToAddNewButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IAddNewItem) { return Visibility.Visible; }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue && !string.IsNullOrEmpty(stringValue))
            {
                if (parameter is string stringParameter)
                {
                    if (stringValue.Equals(stringParameter, StringComparison.OrdinalIgnoreCase)) { return Visibility.Visible; }
                    else { return Visibility.Collapsed; }
                }
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    //public class ContextDescriptionToSearchBoxPlaceholderTextConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        if (value is ContextDescriptor descriptor && descriptor.Description is string description) 
    //        { 
    //            if (description.EndsWith("s", StringComparison.OrdinalIgnoreCase))
    //            {
    //                int index = description.LastIndexOf("s");
    //                description = description.Substring(0, index);
    //            }
    //            return "Find a " + description.ToLower(); 
    //        }
    //        return null;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class RootAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is RootAddress add)
            {
                return $"{add.CityMunicipality}, {add.Province}, {add.Area} {add.Country}";
            }
            return "Unknown Address";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class LeafAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LeafAddress add)
            {
                return $"{add.Lot} {add.Block} {add.Phase}, {add.SubdivisionVillage}, {add.Zone} {add.Barangay}, {add.CityMunicipality}, {add.Province}, {add.Area} {add.ZipCode} {add.Country}";
            }
            return "Unknown Address";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class StringCompareToCheckBoxGlyphConverter : IValueConverter
    {
        private static readonly string checkedGlyph = "";
        private static readonly string uncheckedGlyph = "";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string strValue && parameter is string strParameter)
            {
                if (strValue.Equals(strParameter, StringComparison.OrdinalIgnoreCase))
                { return checkedGlyph; }
            }
            return uncheckedGlyph;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToCheckBoxGlyphConverter : IValueConverter
    {
        private static readonly string checkedGlyph = "";
        private static readonly string uncheckedGlyph = "";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isBoolean) { if (isBoolean) { return checkedGlyph; } }
            return uncheckedGlyph;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToMonthDayYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset date)
            {
                DateTimeFormatter formatter = new DateTimeFormatter("{month.integer(2)}‎/‎{day.integer(2)}/‎{year.full}");
                return formatter.Format(date);
            }
            return "Invalid Date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToAbbreviatedMonthDayYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset date)
            {
                DateTimeFormatter formatter = new DateTimeFormatter("{month.integer(2)}‎/‎{day.integer(2)}/‎{year.abbreviated}");
                return formatter.Format(date);
            }
            return "Invalid Date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToFullMonthDayYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset date)
            {
                DateTimeFormatter formatter = new DateTimeFormatter("month day year");
                return formatter.Format(date);
            }
            return "Invalid Date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToDurationInYearsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset start)
            {
                DateTimeOffset end = parameter is DateTimeOffset offset ? offset : DateTimeOffset.Now;
                TimeSpan span = end.Subtract(start);
                return $"{string.Format("{0,4:N2}", span.TotalDays / 365)} year(s)";
            }
            return "0.00 year(s)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToDurationInDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset start)
            {
                DateTimeOffset end = parameter is DateTimeOffset offset ? offset : DateTimeOffset.Now;
                TimeSpan span = end.Subtract(start);
                return $"{string.Format("{0,4:N2}", span.TotalDays)} day(s)";
            }
            return "0.00 day(s)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToDurationInMinutesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset start)
            {
                DateTimeOffset end = parameter is DateTimeOffset offset ? offset : DateTimeOffset.Now;
                TimeSpan span = end.Subtract(start);
                return $"{string.Format("{0,16:N2}", span.TotalMinutes)} mins.";
            }
            return "0.00 mins.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeOffsetToDurationInHoursConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset start)
            {
                DateTimeOffset end = parameter is DateTimeOffset offset ? offset : DateTimeOffset.Now;
                TimeSpan span = end.Subtract(start);
                return $"{string.Format("{0,8:N2}", span.TotalHours)} hrs.";
            }
            return "0.00 hrs.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonToInitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IPerson p) { return $"{p.FirstName?.ElementAt(0)}{p.LastName?.ElementAt(0)}"; }
            else { return "--"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class PersonToIndexNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IPerson p)
            {
                string nameExtension = string.IsNullOrEmpty(p.NameExtension) ? null : $", {p.NameExtension}";
                return $"{p.LastName}, {p.FirstName} {p.MiddleName?.ElementAt(0)}.{nameExtension}";
            }
            else { return "Unknown"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonToBannerNameConverter : PersonToFullNameConverter, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            string fullname = (string)base.Convert(value, targetType, parameter, language);
            return fullname.ToUpper();
        }
    }

    public class PersonToFullNameConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IPerson p) { return CreateFullName(p); }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string CreateFullName(IPerson p)
        {
            string title = string.IsNullOrEmpty(p.Title) ? null : $"{p.Title} ";
            string nameExtension = string.IsNullOrEmpty(p.NameExtension) ? null : $", {p.NameExtension}";
            return $"{title}{p.FirstName} {p.MiddleName} {p.LastName}{nameExtension}";
        }
    }
}
