using System;
using Windows.UI.Xaml;

namespace Cdln.School.People.Uwp.Models.Addresses
{
    public class LeafAddress : TrunkAddress
    {
        public static readonly DependencyProperty SubdivisionVillageProperty = DependencyProperty.Register(nameof(SubdivisionVillage), typeof(string), typeof(LeafAddress), new PropertyMetadata(null, OnSubdivisionVillagePropertyChanged));
        public static readonly DependencyProperty LotProperty = DependencyProperty.Register(nameof(Lot), typeof(string), typeof(LeafAddress), new PropertyMetadata(null, OnLotPropertyChanged));
        public static readonly DependencyProperty BlockProperty = DependencyProperty.Register(nameof(Block), typeof(string), typeof(LeafAddress), new PropertyMetadata(null, OnBlockPropertyChanged));
        public static readonly DependencyProperty PhaseProperty = DependencyProperty.Register(nameof(Phase), typeof(string), typeof(LeafAddress), new PropertyMetadata(null, OnPhasePropertyChanged));

        private static void OnPhasePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LeafAddress model = (LeafAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M10] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnBlockPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LeafAddress model = (LeafAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M09] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnLotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LeafAddress model = (LeafAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M08] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
        private static void OnSubdivisionVillagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LeafAddress model = (LeafAddress)d;
            string str = (string)e.NewValue;
            model.Markers[M07] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public LeafAddress()
            : this(Guid.NewGuid(), null, null, null, null, null, null, null, null, null, null, null) { }

        public LeafAddress(Guid id, string country, string area, string province, string cityMunicipality, string barangay, string zone,
            string subdivisionVillage, string phase, string block, string lot, string zipCode)
            : base(id, country, area, province, cityMunicipality, barangay, zone, zipCode)
        {
            IsInitializing = true;
            SetValue(LotProperty, lot);
            SetValue(BlockProperty, block);
            SetValue(PhaseProperty, phase);
            SetValue(SubdivisionVillageProperty, subdivisionVillage);
            IsInitializing = false;
        }

        public string Phase
        {
            get { return (string)GetValue(PhaseProperty); }
            set { SetValue(PhaseProperty, value); }
        }
        public string Block
        {
            get { return (string)GetValue(BlockProperty); }
            set { SetValue(BlockProperty, value); }
        }
        public string Lot
        {
            get { return (string)GetValue(LotProperty); }
            set { SetValue(LotProperty, value); }
        }
        public string SubdivisionVillage
        {
            get { return (string)GetValue(SubdivisionVillageProperty); }
            set { SetValue(SubdivisionVillageProperty, value); }
        }
    }
}
