using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(ContactDetails))]
    public class ContactDetails : Model, IContactDetails
    {
        public static readonly DependencyProperty EmailAddressProperty = DependencyProperty.Register(nameof(EmailAddress), typeof(string), typeof(ContactDetails), new PropertyMetadata(null, OnEmailAddressPropertyChanged));
        public static readonly DependencyProperty TelephoneNumberProperty = DependencyProperty.Register(nameof(TelephoneNumber), typeof(string), typeof(ContactDetails), new PropertyMetadata(null, OnTelephoneNumberPropertyChanged));
        public static readonly DependencyProperty MobileNumberProperty = DependencyProperty.Register(nameof(MobileNumber), typeof(string), typeof(ContactDetails), new PropertyMetadata(null, OnMobileNumberPropertyChanged));

        private static void OnMobileNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactDetails model = (ContactDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnTelephoneNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactDetails model = (ContactDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnEmailAddressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactDetails model = (ContactDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public ContactDetails(Guid id)
            : this(id, null, null, null) { }

        public ContactDetails(Guid id, string emailAddress, string telephoneNumber, string mobileNumber)
            : base(id)
        {
            SetValue(EmailAddressProperty, emailAddress);
            SetValue(TelephoneNumberProperty, telephoneNumber);
            SetValue(MobileNumberProperty, mobileNumber);
            IsInitializing = false;
        }

        [DataMember(Name = "mobileNumber")]
        public string MobileNumber
        {
            get { return (string)GetValue(MobileNumberProperty); }
            set { SetValue(MobileNumberProperty, value); }
        }

        [DataMember(Name = "telephoneNumber")]
        public string TelephoneNumber
        {
            get { return (string)GetValue(TelephoneNumberProperty); }
            set { SetValue(TelephoneNumberProperty, value); }
        }

        [DataMember(Name = "emailAddress")]
        public string EmailAddress
        {
            get { return (string)GetValue(EmailAddressProperty); }
            set { SetValue(EmailAddressProperty, value); }
        }
    }
}
