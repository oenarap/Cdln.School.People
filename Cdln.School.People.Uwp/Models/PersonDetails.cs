using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(PersonDetails))]
    public class PersonDetails : Model, IPersonDetails
    {
        public static readonly DependencyProperty SexProperty = DependencyProperty.Register(nameof(Sex), typeof(string), typeof(PersonDetails), new PropertyMetadata(null, OnSexPropertyChanged));
        public static readonly DependencyProperty CivilStatusProperty = DependencyProperty.Register(nameof(CivilStatus), typeof(string), typeof(PersonDetails), new PropertyMetadata(null, OnCivilStatusPropertyChanged));
        public static readonly DependencyProperty OtherCivilStatusProperty = DependencyProperty.Register(nameof(OtherCivilStatus), typeof(string), typeof(PersonDetails), new PropertyMetadata(null, OnOtherCivilStatusPropertyChanged));
        public static readonly DependencyProperty HeightInCentimetersProperty = DependencyProperty.Register(nameof(HeightInCentimeters), typeof(double), typeof(PersonDetails), new PropertyMetadata(0.0d, OnHeightInCentimetersPropertyChanged));
        public static readonly DependencyProperty WeightInKilogramsProperty = DependencyProperty.Register(nameof(WeightInKilograms), typeof(double), typeof(PersonDetails), new PropertyMetadata(0.0d, OnWeightInKilogramsPropertyChanged));
        public static readonly DependencyProperty BloodTypeProperty = DependencyProperty.Register(nameof(BloodType), typeof(string), typeof(PersonDetails), new PropertyMetadata(null, OnBloodTypePropertyChanged));

        private static void OnBloodTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M05] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnWeightInKilogramsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            double val = (double)e.NewValue;
            model.Markers[M04] = (val == 0d);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnHeightInCentimetersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            double val = (double)e.NewValue;
            model.Markers[M03] = (val == 0d);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnOtherCivilStatusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnCivilStatusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnSexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersonDetails model = (PersonDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public PersonDetails(Guid id)
            : this(id, null, null, null, 0d, 0d, null) { }

        public PersonDetails(Guid id, string sex, string civilStatus, string otherCivilStatus, double height, double weight, string bloodType)
            : base(id)
        {
            SetValue(SexProperty, sex);
            SetValue(OtherCivilStatusProperty, otherCivilStatus); //set other civil status prior to civil status
            SetValue(CivilStatusProperty, civilStatus);
            SetValue(HeightInCentimetersProperty, height);
            SetValue(WeightInKilogramsProperty, weight);
            SetValue(BloodTypeProperty, bloodType);
            IsInitializing = false;
        }

        [DataMember(Name = "bloodType")]
        public string BloodType
        {
            get { return (string)GetValue(BloodTypeProperty); }
            set { SetValue(BloodTypeProperty, value); }
        }

        [DataMember(Name = "weightInKilograms")]
        public double WeightInKilograms
        {
            get { return (double)GetValue(WeightInKilogramsProperty); }
            set { SetValue(WeightInKilogramsProperty, value); }
        }

        [DataMember(Name = "heightInCentimeters")]
        public double HeightInCentimeters
        {
            get { return (double)GetValue(HeightInCentimetersProperty); }
            set { SetValue(HeightInCentimetersProperty, value); }
        }

        [DataMember(Name = "otherCivilStatus")]
        public string OtherCivilStatus
        {
            get { return (string)GetValue(OtherCivilStatusProperty); }
            set { SetValue(OtherCivilStatusProperty, value); }
        }

        [DataMember(Name = "civilStatus")]
        public string CivilStatus
        {
            get { return (string)GetValue(CivilStatusProperty); }
            set { SetValue(CivilStatusProperty, value); }
        }

        [DataMember(Name = "sex")]
        public string Sex
        {
            get { return (string)GetValue(SexProperty); }
            set { SetValue(SexProperty, value); }
        }
    }
}
