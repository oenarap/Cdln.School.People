using System;
using Windows.UI.Xaml;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    public sealed class Eligibility : IndexedModel, IEligibility
    {
        public static readonly DependencyProperty EligibilityNameProperty = DependencyProperty.Register(nameof(EligibilityName), typeof(string), typeof(Eligibility), new PropertyMetadata(null, OnEligibilityNamePropertyChanged));
        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register(nameof(Rating), typeof(double), typeof(Eligibility), new PropertyMetadata(0d, OnRatingPropertyChanged));
        public static readonly DependencyProperty LicenseNumberIfApplicableProperty = DependencyProperty.Register(nameof(LicenseNumberIfApplicable), typeof(string), typeof(Eligibility), new PropertyMetadata(null, OnLicenseNumberIfApplicablePropertyChanged));
        public static readonly DependencyProperty DateOfExamOrConfermentProperty = DependencyProperty.Register(nameof(DateOfExamOrConferment), typeof(DateTimeOffset?), typeof(Eligibility), new PropertyMetadata(null, OnDateOfExamOrConfermentPropertyChanged));
        public static readonly DependencyProperty PlaceOfExamConfermentProperty = DependencyProperty.Register(nameof(PlaceOfExamConferment), typeof(Guid?), typeof(Eligibility), new PropertyMetadata(null, OnPlaceOfExamConfermentPropertyChanged));
        public static readonly DependencyProperty LicenseDateOfReleaseProperty = DependencyProperty.Register(nameof(LicenseDateOfRelease), typeof(DateTimeOffset?), typeof(Eligibility), new PropertyMetadata(null, OnLicenseDateOfReleasePropertyChanged));

        private static void OnLicenseDateOfReleasePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            DateTimeOffset? date = (DateTimeOffset?)e.NewValue;
            model.Markers[M05] = (date == null);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnPlaceOfExamConfermentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            Guid? id = (Guid?)e.NewValue;
            model.Markers[M04] = (id == null || id == Guid.Empty);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnDateOfExamOrConfermentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            DateTimeOffset? date = (DateTimeOffset?)e.NewValue;
            model.Markers[M03] = (date == null);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnLicenseNumberIfApplicablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnRatingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            double rating = (double)e.NewValue;
            model.Markers[M01] = (rating == 0d);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnEligibilityNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Eligibility model = (Eligibility)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public Eligibility(Guid id)
            : this(id, Guid.Empty, null, 0d, null, null, null, null) { }

        public Eligibility(Guid id, Guid index, string eligibilityName, double rating, string licenseNumber,
            DateTimeOffset? dateOfExamOrConferment, DateTimeOffset? licenseDateOfRelease, Guid? placeOfExamConferment)
            : base(id, index)
        {
            SetValue(EligibilityNameProperty, eligibilityName);
            SetValue(RatingProperty, rating);
            SetValue(LicenseNumberIfApplicableProperty, licenseNumber);
            SetValue(DateOfExamOrConfermentProperty, dateOfExamOrConferment);
            SetValue(LicenseDateOfReleaseProperty, licenseDateOfRelease);
            SetValue(PlaceOfExamConfermentProperty, placeOfExamConferment);
            IsInitializing = false;
        }

        public string EligibilityName
        {
            get { return (string)GetValue(EligibilityNameProperty); }
            set { SetValue(EligibilityNameProperty, value); }
        }
        public double Rating
        {
            get { return (double)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }
        public DateTimeOffset? DateOfExamOrConferment
        {
            get { return (DateTimeOffset?)GetValue(DateOfExamOrConfermentProperty); }
            set { SetValue(DateOfExamOrConfermentProperty, value); }
        }
        public Guid? PlaceOfExamConferment
        {
            get { return (Guid?)GetValue(PlaceOfExamConfermentProperty); }
            set { SetValue(PlaceOfExamConfermentProperty, value); }
        }
        public string LicenseNumberIfApplicable
        {
            get { return (string)GetValue(LicenseNumberIfApplicableProperty); }
            set { SetValue(LicenseNumberIfApplicableProperty, value); }
        }
        public DateTimeOffset? LicenseDateOfRelease
        {
            get { return (DateTimeOffset?)GetValue(DateOfExamOrConfermentProperty); }
            set { SetValue(DateOfExamOrConfermentProperty, value); }
        }
    }
}
