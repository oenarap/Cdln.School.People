using System;
using Windows.UI.Xaml;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    public class Faqs : Model, IFaqs
    {
        public static readonly DependencyProperty IsRelatedToAuthorityThirdDegreeProperty = DependencyProperty.Register(nameof(IsRelatedToAuthorityThirdDegree), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty IsRelatedToAuthorityFourthDegreeProperty = DependencyProperty.Register(nameof(IsRelatedToAuthorityFourthDegree), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty RelationshipToAuthorityDetailsProperty = DependencyProperty.Register(nameof(RelationshipToAuthorityDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnRelationshipToAuthorityDetailsPropertyChanged));
        public static readonly DependencyProperty IsGuiltyOfAdministrativeOffenseProperty = DependencyProperty.Register(nameof(IsGuiltyOfAdministrativeOffense), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty AdministrativeOffenseDetailsProperty = DependencyProperty.Register(nameof(AdministrativeOffenseDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnAdministrativeOffenseDetailsPropertyChanged));
        public static readonly DependencyProperty WasCriminallyChargedProperty = DependencyProperty.Register(nameof(WasCriminallyCharged), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty CriminalChargesFilingDateProperty = DependencyProperty.Register(nameof(CriminalChargesFilingDate), typeof(DateTimeOffset?), typeof(Faqs), new PropertyMetadata(null, OnCriminalChargesFilingDatePropertyChanged));
        public static readonly DependencyProperty CriminalChargesCaseStatusProperty = DependencyProperty.Register(nameof(CriminalChargesCaseStatus), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnCriminalChargesCaseStatusPropertyChanged));
        public static readonly DependencyProperty WasConvictedProperty = DependencyProperty.Register(nameof(WasConvicted), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty ConvictionDetailsProperty = DependencyProperty.Register(nameof(ConvictionDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnConvictionDetailsPropertyChanged));
        public static readonly DependencyProperty WasSeparatedFromServiceProperty = DependencyProperty.Register(nameof(WasSeparatedFromService), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty SeparationFromServiceDetailsProperty = DependencyProperty.Register(nameof(SeparationFromServiceDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnSeparationFromServiceDetailsPropertyChanged));
        public static readonly DependencyProperty WasNatlOrLocalElectionCandidateProperty = DependencyProperty.Register(nameof(WasNatlOrLocalElectionCandidate), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty NatlOrLocalElectionCandidacyDetailsProperty = DependencyProperty.Register(nameof(NatlOrLocalElectionCandidacyDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnNatlOrLocalElectionCandidacyDetailsPropertyChanged));
        public static readonly DependencyProperty HasResignedForCandidacyProperty = DependencyProperty.Register(nameof(HasResignedForCandidacy), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty ResignationForCandidacyDetailsProperty = DependencyProperty.Register(nameof(ResignationForCandidacyDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnResignationForCandidacyDetailsPropertyChanged));
        public static readonly DependencyProperty HasAcquiredImmigrantStatusProperty = DependencyProperty.Register(nameof(HasAcquiredImmigrantStatus), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty OriginCountryProperty = DependencyProperty.Register(nameof(OriginCountry), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnOriginCountryPropertyChanged));
        public static readonly DependencyProperty IsIndigenousGroupMemberProperty = DependencyProperty.Register(nameof(IsIndigenousGroupMember), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty IndigenousGroupMembershipDetailsProperty = DependencyProperty.Register(nameof(IndigenousGroupMembershipDetails), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnIndigenousGroupMembershipDetailsPropertyChanged));
        public static readonly DependencyProperty IsDifferentlyAbledProperty = DependencyProperty.Register(nameof(IsDifferentlyAbled), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty DifferentlyAbledIdNumberProperty = DependencyProperty.Register(nameof(DifferentlyAbledIdNumber), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnDifferentlyAbledIdNumberPropertyChanged));
        public static readonly DependencyProperty IsSoloParentProperty = DependencyProperty.Register(nameof(IsSoloParent), typeof(bool), typeof(Faqs), new PropertyMetadata(false, OnBooleanPropertyChanged));
        public static readonly DependencyProperty SoloParentIdNumberProperty = DependencyProperty.Register(nameof(SoloParentIdNumber), typeof(string), typeof(Faqs), new PropertyMetadata(null, OnSoloParentIdNumberPropertyChanged));

        private static void OnRelationshipToAuthorityDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M11] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnAdministrativeOffenseDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M10] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnCriminalChargesFilingDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M09] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnCriminalChargesCaseStatusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M08] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnConvictionDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M07] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnSeparationFromServiceDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M06] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnNatlOrLocalElectionCandidacyDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M05] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnResignationForCandidacyDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M04] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnOriginCountryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M03] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnIndigenousGroupMembershipDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnDifferentlyAbledIdNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnSoloParentIdNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnBooleanPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Faqs model = (Faqs)d;
            model.Markers[Default] = model.IsInitializing;
        }

        public Faqs(Guid id)
            : this(id, false, false, null, false, null, false, null, null, false, null, false, null, false, null, false, null, false, null, false, null, false, null, false, null) { }
        public Faqs(Guid id, bool isRelatedToAuthorityThirdDegree, bool isRelatedToAuthorityFourthDegree, string relationshipToAuthorityDetails,
            bool isGuiltyOfAdministrativeOffense, string administrativeOffenseDetails, bool wasCriminallyCharged, DateTimeOffset? criminalChargesFilingDate,
            string criminalChargesCaseStatus, bool wasConvicted, string convictionDetails, bool wasSeparatedFromService, string separationFromServiceDetails,
            bool wasNatlOrLocalElectionCandidate, string natlOrLocalElectionCandidacyDetails, bool hasResignedForCandidacy, string resignationForCandidacyDetails,
            bool hasAcquiredImmigrantStatus, string originCountry, bool isIndigenousGroupMember, string indigenousGroupMembershipDetails, bool isDifferentlyAbled,
            string differentlyAbledIdNumber, bool isSoloParent, string soloParentIdNumber)
            : base(id)
        {
            SetValue(SoloParentIdNumberProperty, soloParentIdNumber);
            SetValue(IsSoloParentProperty, isSoloParent);
            SetValue(DifferentlyAbledIdNumberProperty, differentlyAbledIdNumber);
            SetValue(IsDifferentlyAbledProperty, isDifferentlyAbled);
            SetValue(IndigenousGroupMembershipDetailsProperty, indigenousGroupMembershipDetails);
            SetValue(IsIndigenousGroupMemberProperty, isIndigenousGroupMember);
            SetValue(OriginCountryProperty, originCountry);
            SetValue(HasAcquiredImmigrantStatusProperty, hasAcquiredImmigrantStatus);
            SetValue(ResignationForCandidacyDetailsProperty, resignationForCandidacyDetails);
            SetValue(HasResignedForCandidacyProperty, hasResignedForCandidacy);
            SetValue(NatlOrLocalElectionCandidacyDetailsProperty, natlOrLocalElectionCandidacyDetails);
            SetValue(WasNatlOrLocalElectionCandidateProperty, wasNatlOrLocalElectionCandidate);
            SetValue(SeparationFromServiceDetailsProperty, separationFromServiceDetails);
            SetValue(WasSeparatedFromServiceProperty, wasSeparatedFromService);
            SetValue(ConvictionDetailsProperty, convictionDetails);
            SetValue(WasConvictedProperty, wasConvicted);
            SetValue(CriminalChargesCaseStatusProperty, criminalChargesCaseStatus);
            SetValue(CriminalChargesFilingDateProperty, criminalChargesFilingDate);
            SetValue(WasCriminallyChargedProperty, wasCriminallyCharged);
            SetValue(AdministrativeOffenseDetailsProperty, administrativeOffenseDetails);
            SetValue(IsGuiltyOfAdministrativeOffenseProperty, isGuiltyOfAdministrativeOffense);
            SetValue(RelationshipToAuthorityDetailsProperty, relationshipToAuthorityDetails);
            SetValue(IsRelatedToAuthorityFourthDegreeProperty, isRelatedToAuthorityFourthDegree);
            SetValue(IsRelatedToAuthorityThirdDegreeProperty, isRelatedToAuthorityThirdDegree);
        }

        public bool IsRelatedToAuthorityThirdDegree
        {
            get { return (bool)GetValue(IsRelatedToAuthorityThirdDegreeProperty); }
            set { SetValue(IsRelatedToAuthorityThirdDegreeProperty, value); }
        }
        public bool IsRelatedToAuthorityFourthDegree
        {
            get { return (bool)GetValue(IsRelatedToAuthorityFourthDegreeProperty); }
            set { SetValue(IsRelatedToAuthorityFourthDegreeProperty, value); }
        }
        public string RelationshipToAuthorityDetails
        {
            get { return (string)GetValue(RelationshipToAuthorityDetailsProperty); }
            set { SetValue(RelationshipToAuthorityDetailsProperty, value); }
        }
        public bool IsGuiltyOfAdministrativeOffense
        {
            get { return (bool)GetValue(IsGuiltyOfAdministrativeOffenseProperty); }
            set { SetValue(IsGuiltyOfAdministrativeOffenseProperty, value); }
        }
        public string AdministrativeOffenseDetails
        {
            get { return (string)GetValue(AdministrativeOffenseDetailsProperty); }
            set { SetValue(AdministrativeOffenseDetailsProperty, value); }
        }
        public bool WasCriminallyCharged
        {
            get { return (bool)GetValue(WasCriminallyChargedProperty); }
            set { SetValue(WasCriminallyChargedProperty, value); }
        }
        public DateTimeOffset? CriminalChargesFilingDate
        {
            get { return (DateTimeOffset?)GetValue(CriminalChargesFilingDateProperty); }
            set { SetValue(CriminalChargesFilingDateProperty, value); }
        }
        public string CriminalChargesCaseStatus
        {
            get { return (string)GetValue(CriminalChargesCaseStatusProperty); }
            set { SetValue(CriminalChargesCaseStatusProperty, value); }
        }
        public bool WasConvicted
        {
            get { return (bool)GetValue(WasConvictedProperty); }
            set { SetValue(WasConvictedProperty, value); }
        }
        public string ConvictionDetails
        {
            get { return (string)GetValue(ConvictionDetailsProperty); }
            set { SetValue(ConvictionDetailsProperty, value); }
        }
        public bool WasSeparatedFromService
        {
            get { return (bool)GetValue(WasSeparatedFromServiceProperty); }
            set { SetValue(WasSeparatedFromServiceProperty, value); }
        }
        public string SeparationFromServiceDetails
        {
            get { return (string)GetValue(SeparationFromServiceDetailsProperty); }
            set { SetValue(SeparationFromServiceDetailsProperty, value); }
        }
        public bool WasNatlOrLocalElectionCandidate
        {
            get { return (bool)GetValue(WasNatlOrLocalElectionCandidateProperty); }
            set { SetValue(WasNatlOrLocalElectionCandidateProperty, value); }
        }
        public string NatlOrLocalElectionCandidacyDetails
        {
            get { return (string)GetValue(NatlOrLocalElectionCandidacyDetailsProperty); }
            set { SetValue(NatlOrLocalElectionCandidacyDetailsProperty, value); }
        }
        public bool HasResignedForCandidacy
        {
            get { return (bool)GetValue(HasResignedForCandidacyProperty); }
            set { SetValue(HasResignedForCandidacyProperty, value); }
        }
        public string ResignationForCandidacyDetails
        {
            get { return (string)GetValue(ResignationForCandidacyDetailsProperty); }
            set { SetValue(ResignationForCandidacyDetailsProperty, value); }
        }
        public bool HasAcquiredImmigrantStatus
        {
            get { return (bool)GetValue(HasAcquiredImmigrantStatusProperty); }
            set { SetValue(HasAcquiredImmigrantStatusProperty, value); }
        }
        public string OriginCountry
        {
            get { return (string)GetValue(OriginCountryProperty); }
            set { SetValue(OriginCountryProperty, value); }
        }
        public bool IsIndigenousGroupMember
        {
            get { return (bool)GetValue(IsIndigenousGroupMemberProperty); }
            set { SetValue(IsIndigenousGroupMemberProperty, value); }
        }
        public string IndigenousGroupMembershipDetails
        {
            get { return (string)GetValue(IndigenousGroupMembershipDetailsProperty); }
            set { SetValue(IndigenousGroupMembershipDetailsProperty, value); }
        }
        public bool IsDifferentlyAbled
        {
            get { return (bool)GetValue(IsDifferentlyAbledProperty); }
            set { SetValue(IsDifferentlyAbledProperty, value); }
        }
        public string DifferentlyAbledIdNumber
        {
            get { return (string)GetValue(DifferentlyAbledIdNumberProperty); }
            set { SetValue(DifferentlyAbledIdNumberProperty, value); }
        }
        public bool IsSoloParent
        {
            get { return (bool)GetValue(IsSoloParentProperty); }
            set { SetValue(IsSoloParentProperty, value); }
        }
        public string SoloParentIdNumber
        {
            get { return (string)GetValue(SoloParentIdNumberProperty); }
            set { SetValue(SoloParentIdNumberProperty, value); }
        }
    }
}
