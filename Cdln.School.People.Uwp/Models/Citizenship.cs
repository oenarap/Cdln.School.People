using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(Citizenship))]
    public class Citizenship : Model, ICitizenship
    {
        public static readonly DependencyProperty DualCitizenshipModeProperty = DependencyProperty.Register(nameof(DualCitizenshipMode), typeof(string), typeof(Citizenship), new PropertyMetadata(null, OnDualCitizenshipModePropertyChanged));
        public static readonly DependencyProperty DualCitizenshipProperty = DependencyProperty.Register(nameof(DualCitizenship), typeof(string), typeof(Citizenship), new PropertyMetadata(null, OnDualCitizenshipPropertyChanged));
        public static readonly DependencyProperty CountryProperty = DependencyProperty.Register(nameof(Country), typeof(string), typeof(Citizenship), new PropertyMetadata(null, OnCountryPropertyChanged));

        private static void OnCountryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Citizenship model = (Citizenship)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnDualCitizenshipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Citizenship model = (Citizenship)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnDualCitizenshipModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Citizenship model = (Citizenship)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public Citizenship(Guid id)
            : this(id, null, null, null) { }

        public Citizenship(Guid id, string dualCitizenshipMode, string dualCitizenship, string country)
            : base(id)
        {
            SetValue(CountryProperty, country); //set country & dual citizenship prior to citizenship
            SetValue(DualCitizenshipProperty, dualCitizenship);
            SetValue(DualCitizenshipModeProperty, dualCitizenshipMode);
            IsInitializing = false;
        }

        [DataMember(Name = "country")]
        public string Country
        {
            get { return (string)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        [DataMember(Name = "dualCitizenship")]
        public string DualCitizenship
        {
            get { return (string)GetValue(DualCitizenshipProperty); }
            set { SetValue(DualCitizenshipProperty, value); }
        }

        [DataMember(Name = "dualCitizenshipMode")]
        public string DualCitizenshipMode
        {
            get { return (string)GetValue(DualCitizenshipModeProperty); }
            set { SetValue(DualCitizenshipModeProperty, value); }
        }
    }
}
