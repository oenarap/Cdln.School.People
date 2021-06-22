using System;
using Windows.UI.Xaml;

namespace Cdln.School.People.Uwp.Models.Addresses
{
    public class BranchAddress : TrunkAddress
    {
        public static readonly DependencyProperty RoomFloorProperty = DependencyProperty.Register(nameof(RoomFloor), typeof(string), typeof(BranchAddress), new PropertyMetadata(null, OnRoomFloorPropertyChanged));
        public static readonly DependencyProperty BuildingProperty = DependencyProperty.Register(nameof(Building), typeof(string), typeof(BranchAddress), new PropertyMetadata(null, OnDependencyPropertyChanged));
        public static readonly DependencyProperty Street1Property = DependencyProperty.Register(nameof(Street1), typeof(string), typeof(BranchAddress), new PropertyMetadata(null, OnStreet1PropertyChanged));
        public static readonly DependencyProperty Street2Property = DependencyProperty.Register(nameof(Street2), typeof(string), typeof(BranchAddress), new PropertyMetadata(null, OnStreet2PropertyChanged));

        private static void OnRoomFloorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BranchAddress model = (BranchAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M10] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BranchAddress model = (BranchAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M09] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnStreet1PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BranchAddress model = (BranchAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M08] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnStreet2PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BranchAddress model = (BranchAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M07] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public BranchAddress()
            : this(Guid.Empty, null, null, null, null, null, null, null, null, null, null, null) { }

        public BranchAddress(Guid id, string country, string area, string province, string cityMunicipality, string barangay, string zone,
            string street1, string street2, string building, string roomFloor, string zipCode)
            : base(id, country, area, province, cityMunicipality, barangay, zone, zipCode)
        {
            IsInitializing = true;
            SetValue(RoomFloorProperty, roomFloor);
            SetValue(BuildingProperty, building);
            SetValue(Street2Property, street2);
            SetValue(Street1Property, street1);
            IsInitializing = false;
        }

        public string RoomFloor
        {
            get { return (string)GetValue(RoomFloorProperty); }
            set { SetValue(RoomFloorProperty, value); }
        }
        public string Building
        {
            get { return (string)GetValue(BuildingProperty); }
            set { SetValue(BuildingProperty, value); }
        }
        public string Street1
        {
            get { return (string)GetValue(Street1Property); }
            set { SetValue(Street1Property, value); }
        }
        public string Street2
        {
            get { return (string)GetValue(Street2Property); }
            set { SetValue(Street2Property, value); }
        }
    }
}
