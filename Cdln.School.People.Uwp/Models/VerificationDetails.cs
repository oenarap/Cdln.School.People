using System;
using Windows.UI.Xaml;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    public sealed class VerificationDetails : Model, IVerificationDetails
    {
        public static readonly DependencyProperty CommunityTaxCertificateNumberProperty = DependencyProperty.Register(nameof(CommunityTaxCertificateNumber), typeof(string), typeof(VerificationDetails), new PropertyMetadata(null));
        public static readonly DependencyProperty CommunityTaxCertificateIssuanceDateProperty = DependencyProperty.Register(nameof(CommunityTaxCertificateIssuanceDate), typeof(DateTimeOffset?), typeof(VerificationDetails), new PropertyMetadata(DateTimeOffset.Now));
        public static readonly DependencyProperty PdsDateAccomplishedProperty = DependencyProperty.Register(nameof(PdsDateAccomplished), typeof(DateTimeOffset?), typeof(VerificationDetails), new PropertyMetadata(DateTimeOffset.Now));

        public VerificationDetails()
            : this(Guid.NewGuid(), null, null, null) { }
        public VerificationDetails(Guid id, string communityTaxCertificateNumber, DateTimeOffset? communityTaxCertificateIssuanceDate, DateTimeOffset? pds_accomplished_on)
            : base(id)
        {
            SetValue(CommunityTaxCertificateNumberProperty, communityTaxCertificateNumber);
            SetValue(CommunityTaxCertificateIssuanceDateProperty, communityTaxCertificateIssuanceDate ?? DateTimeOffset.Now);
            SetValue(PdsDateAccomplishedProperty, pds_accomplished_on ?? DateTimeOffset.Now);
            IsInitializing = false;
        }

        public DateTimeOffset? PdsDateAccomplished
        {
            get { return (DateTimeOffset)GetValue(PdsDateAccomplishedProperty); }
            set { SetValue(PdsDateAccomplishedProperty, value); }
        }
        public DateTimeOffset? CommunityTaxCertificateIssuanceDate
        {
            get { return (DateTimeOffset?)GetValue(CommunityTaxCertificateIssuanceDateProperty); }
            set { SetValue(CommunityTaxCertificateIssuanceDateProperty, value); }
        }
        public string CommunityTaxCertificateNumber
        {
            get { return (string)GetValue(CommunityTaxCertificateNumberProperty); }
            set { SetValue(CommunityTaxCertificateNumberProperty, value); }
        }
    }
}
