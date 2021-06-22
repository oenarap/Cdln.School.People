using System;
using Windows.UI.Xaml;

namespace Cdln.School.People.Uwp.Models.Addresses
{
    public class TrunkAddress : RootAddress
    {
        public static readonly DependencyProperty ZoneProperty = DependencyProperty.Register(nameof(Zone), typeof(string), typeof(TrunkAddress), new PropertyMetadata(null, OnZonePropertyChanged));
        public static readonly DependencyProperty BarangayProperty = DependencyProperty.Register(nameof(Barangay), typeof(string), typeof(TrunkAddress), new PropertyMetadata(null, OnBarangayPropertyChanged));
        public static readonly DependencyProperty ZipCodeProperty = DependencyProperty.Register(nameof(ZipCode), typeof(string), typeof(TrunkAddress), new PropertyMetadata(null, OnZipCodePropertyChanged));

        private static void OnZipCodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TrunkAddress data = (TrunkAddress)d;
            string str = (string)e.NewValue;
            data.Markers[M06] = string.IsNullOrEmpty(str);
            data.Markers[Default] = data.IsInitializing;
        }
        private static void OnZonePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TrunkAddress data = (TrunkAddress)d;
            string str = (string)e.NewValue;
            data.Markers[M05] = string.IsNullOrEmpty(str);
            data.Markers[Default] = data.IsInitializing;
        }
        private static void OnBarangayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TrunkAddress data = (TrunkAddress)d;
            string str = (string)e.NewValue;
            data.Markers[M04] = string.IsNullOrEmpty(str);
            data.Markers[Default] = data.IsInitializing;
        }

        public TrunkAddress()
            : this(Guid.NewGuid(), null, null, null, null, null, null, null) { }

        public TrunkAddress(Guid id, string country, string area, string province, string cityMunicipality, string barangay, string zone, string zipCode)
            : base(id, country, area, province, cityMunicipality)
        {
            IsInitializing = true;
            SetValue(BarangayProperty, barangay);
            SetValue(ZoneProperty, zone);
            SetValue(ZipCodeProperty, zipCode);
            IsInitializing = false;
        }

        public string Zone
        {
            get { return (string)GetValue(ZoneProperty); }
            set { SetValue(ZoneProperty, value); }
        }
        public string Barangay
        {
            get { return (string)GetValue(BarangayProperty); }
            set { SetValue(BarangayProperty, value); }
        }
        public string ZipCode
        {
            get { return (string)GetValue(ZipCodeProperty); }
            set { SetValue(ZipCodeProperty, value); }
        }
    }
}
