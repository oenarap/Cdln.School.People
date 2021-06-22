using System;
using Windows.UI.Xaml;

namespace Cdln.School.People.Uwp.Models.Addresses
{
    public class RootAddress : Model
    {
        public static readonly DependencyProperty CityMunicipalityProperty = DependencyProperty.Register(nameof(CityMunicipality), typeof(string), typeof(RootAddress), new PropertyMetadata(null, OnCityMunicipalityPropertyChanged));
        public static readonly DependencyProperty ProvinceProperty = DependencyProperty.Register(nameof(Province), typeof(string), typeof(RootAddress), new PropertyMetadata(null, OnProvincePropertyChanged));
        public static readonly DependencyProperty AreaProperty = DependencyProperty.Register(nameof(Area), typeof(string), typeof(RootAddress), new PropertyMetadata(null, OnAreaPropertyChanged));
        public static readonly DependencyProperty CountryProperty = DependencyProperty.Register(nameof(Country), typeof(string), typeof(RootAddress), new PropertyMetadata(null, OnCountryPropertyChanged));

        private static void OnCountryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RootAddress model = (RootAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M03] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnAreaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RootAddress model = (RootAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnProvincePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RootAddress model = (RootAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnCityMunicipalityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RootAddress model = (RootAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public RootAddress()
            : this(Guid.NewGuid()) { }

        public RootAddress(Guid id)
            : this(id, null, null, null, null) { }

        public RootAddress(Guid id, string country, string area, string province, string cityMunicipality)
            : base(id)
        {
            SetValue(CountryProperty, country);
            SetValue(AreaProperty, area);
            SetValue(ProvinceProperty, province);
            SetValue(CityMunicipalityProperty, cityMunicipality);
            IsInitializing = false;
        }

        public string CityMunicipality
        {
            get { return (string)GetValue(CityMunicipalityProperty); }
            set { SetValue(CityMunicipalityProperty, value); }
        }
        public string Province
        {
            get { return (string)GetValue(ProvinceProperty); }
            set { SetValue(ProvinceProperty, value); }
        }
        public string Area
        {
            get { return (string)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }
        public string Country
        {
            get { return (string)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }
    }
}
