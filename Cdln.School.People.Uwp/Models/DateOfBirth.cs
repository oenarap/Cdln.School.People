using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(DateOfBirth))]
    public class DateOfBirth : Model, IDateOfBirth
    {
        public static readonly DependencyProperty BirthdateProperty = DependencyProperty.Register(nameof(Birthdate), typeof(DateTimeOffset), typeof(DateOfBirth), new PropertyMetadata(null, OnBirthdatePropertyChanged));

        private static void OnBirthdatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateOfBirth model = (DateOfBirth)d;
            model.Markers[M00] = e.NewValue is DateTimeOffset;
            model.Markers[Default] = model.IsInitializing;
        }

        public DateOfBirth(Guid id)
            : this(id, null) { }

        public DateOfBirth(Guid id, DateTimeOffset? birthdate)
            : base(id)
        {
            SetValue(BirthdateProperty, birthdate ?? DateTimeOffset.Now);
            IsInitializing = false;
        }

        [DataMember(Name = "birthdate")]
        public DateTimeOffset? Birthdate
        {
            get { return (DateTimeOffset)GetValue(BirthdateProperty); }
            set { SetValue(BirthdateProperty, value); }
        }
    }
}
