using System;
using Windows.UI.Xaml;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    public sealed class Education : IndexedModel, IEducation
    {
        public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(nameof(Level), typeof(string), typeof(Education), new PropertyMetadata(null, OnLevelPropertyChanged));
        public static readonly DependencyProperty SchoolNameProperty = DependencyProperty.Register(nameof(SchoolName), typeof(string), typeof(Education), new PropertyMetadata(null, OnSchoolNamePropertyChanged));
        public static readonly DependencyProperty DegreeCourseProperty = DependencyProperty.Register(nameof(DegreeCourse), typeof(string), typeof(Education), new PropertyMetadata(null, OnDegreeCoursePropertyChanged));
        public static readonly DependencyProperty IfGraduatedYearGraduatedProperty = DependencyProperty.Register(nameof(IfGraduatedYearGraduated), typeof(string), typeof(Education), new PropertyMetadata(null, OnIfGraduatedYearGraduatedPropertyChanged));
        public static readonly DependencyProperty IfNotGraduatedHighestLevelOrUnitsEarnedProperty = DependencyProperty.Register(nameof(IfNotGraduatedHighestLevelOrUnitsEarned), typeof(string), typeof(Education), new PropertyMetadata(null, OnIfNotGraduatedHighestLevelOrUnitsEarnedPropertyChanged));
        public static readonly DependencyProperty ScholarshipOrHonorsReceivedProperty = DependencyProperty.Register(nameof(ScholarshipOrHonorsReceived), typeof(string), typeof(Education), new PropertyMetadata(null, OnScholarshipOrHonorsReceivedPropertyChanged));
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(nameof(StartDate), typeof(DateTimeOffset?), typeof(Education), new PropertyMetadata(null, OnStartDatePropertyChanged));
        public static readonly DependencyProperty IsOngoingProperty = DependencyProperty.Register(nameof(IsOngoing), typeof(bool), typeof(Education), new PropertyMetadata(false, OnIsOngoingPropertyChanged));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(nameof(EndDate), typeof(DateTimeOffset?), typeof(Education), new PropertyMetadata(null, OnEndDatePropertyChanged));

        private static void OnIsOngoingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;
            if ((bool)e.NewValue)
            {
                model.SetValue(EndDateProperty, DateTimeOffset.Now);
                return;
            }
            model.SetValue(EndDateProperty, null);
        }

        private static void OnEndDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;

            if (model.IsOngoing)
            {
                // verify new value
                if (e.NewValue is DateTimeOffset newDate && newDate.Date != DateTimeOffset.Now.Date)
                {
                    // revert to old value. loop back will occur
                    model.SetValue(EndDateProperty, e.OldValue);
                    return;
                }
            }

            DateTimeOffset? date = (DateTimeOffset?)e.NewValue;
            model.Markers[M07] = (date == null);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnStartDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;
            DateTimeOffset? date = (DateTimeOffset?)e.NewValue;
            model.Markers[M06] = (date == null);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnScholarshipOrHonorsReceivedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: impose business logic
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M05] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnIfNotGraduatedHighestLevelOrUnitsEarnedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: impose business logic
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M04] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnIfGraduatedYearGraduatedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: impose business logic
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M03] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnLevelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnSchoolNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnDegreeCoursePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Education model = (Education)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public Education(Guid id)
            : this(id, Guid.Empty, null, null, null, null, null, null, null, false, null) { }

        public Education(Guid id, Guid index, string level, string schoolName, string course, string ifGraduatedYearGraduated, string ifNotGraduatedHighestLevelOrUnitsEarned,
            DateTimeOffset? startDate, DateTimeOffset? endDate, bool isOngoing, string scholarshipOrHonorsReceived)
            : base(id, index)
        {
            SetValue(IsOngoingProperty, isOngoing);  // constraint

            SetValue(LevelProperty, level);
            SetValue(SchoolNameProperty, schoolName);
            SetValue(DegreeCourseProperty, course);
            SetValue(IfGraduatedYearGraduatedProperty, ifGraduatedYearGraduated);
            SetValue(IfNotGraduatedHighestLevelOrUnitsEarnedProperty, ifNotGraduatedHighestLevelOrUnitsEarned);
            SetValue(ScholarshipOrHonorsReceivedProperty, scholarshipOrHonorsReceived);
            SetValue(StartDateProperty, startDate);
            SetValue(EndDateProperty, endDate);
            IsInitializing = false;
        }

        public string IfGraduatedYearGraduated
        {
            get { return (string)GetValue(IfGraduatedYearGraduatedProperty); }
            set { SetValue(IfGraduatedYearGraduatedProperty, value); }
        }
        public string ScholarshipOrHonorsReceived
        {
            get { return (string)GetValue(ScholarshipOrHonorsReceivedProperty); }
            set { SetValue(ScholarshipOrHonorsReceivedProperty, value); }
        }
        public string IfNotGraduatedHighestLevelOrUnitsEarned
        {
            get { return (string)GetValue(IfNotGraduatedHighestLevelOrUnitsEarnedProperty); }
            set { SetValue(IfNotGraduatedHighestLevelOrUnitsEarnedProperty, value); }
        }
        public string DegreeCourse
        {
            get { return (string)GetValue(DegreeCourseProperty); }
            set { SetValue(DegreeCourseProperty, value); }
        }
        public string SchoolName
        {
            get { return (string)GetValue(SchoolNameProperty); }
            set { SetValue(SchoolNameProperty, value); }
        }
        public string Level
        {
            get { return (string)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }
        public bool IsOngoing
        {
            get { return (bool)GetValue(IsOngoingProperty); }
            set { SetValue(IsOngoingProperty, value); }
        }
        public DateTimeOffset? EndDate
        {
            get { return (DateTimeOffset?)GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }
        public DateTimeOffset? StartDate
        {
            get { return (DateTimeOffset?)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }
    }
}
