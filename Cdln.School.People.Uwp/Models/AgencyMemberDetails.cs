using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(AgencyMemberDetails))]
    public class AgencyMemberDetails : Model, IAgencyMemberDetails
    {
        public static readonly DependencyProperty AgencyIdProperty = DependencyProperty.Register(nameof(AgencyId), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnAgencyIdPropertyChanged));
        public static readonly DependencyProperty GsisIdNumberProperty = DependencyProperty.Register(nameof(GsisIdNumber), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnGsisIdNumberPropertyChanged));
        public static readonly DependencyProperty PagIbigIdNumberProperty = DependencyProperty.Register(nameof(PagIbigIdNumber), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnPagIbigIdNumberPropertyChanged));
        public static readonly DependencyProperty PhilhealthNumberProperty = DependencyProperty.Register(nameof(PhilhealthNumber), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnPhilhealthNumberPropertyChanged));
        public static readonly DependencyProperty SssNumberProperty = DependencyProperty.Register(nameof(SssNumber), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnSssNumberPropertyChanged));
        public static readonly DependencyProperty TinProperty = DependencyProperty.Register(nameof(Tin), typeof(string), typeof(AgencyMemberDetails), new PropertyMetadata(null, OnTinPropertyChanged));

        private static void OnTinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M05] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnSssNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M04] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnPhilhealthNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M03] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnPagIbigIdNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnGsisIdNumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnAgencyIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AgencyMemberDetails model = (AgencyMemberDetails)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public AgencyMemberDetails(Guid id)
            : this(id, null, null, null, null, null, null) { }

        public AgencyMemberDetails(Guid id, string agencyId, string gsisId, string pagibigId, string philhealthId, string sssId, string tinId)
            : base(id)
        {
            SetValue(AgencyIdProperty, agencyId);
            SetValue(GsisIdNumberProperty, gsisId);
            SetValue(PagIbigIdNumberProperty, pagibigId);
            SetValue(PhilhealthNumberProperty, philhealthId);
            SetValue(SssNumberProperty, sssId);
            SetValue(TinProperty, tinId);
            IsInitializing = false;
        }

        [DataMember(Name = "tin")]
        public string Tin
        {
            get { return (string)GetValue(TinProperty); }
            set { SetValue(TinProperty, value); }
        }

        [DataMember(Name = "sssNumber")]
        public string SssNumber
        {
            get { return (string)GetValue(SssNumberProperty); }
            set { SetValue(SssNumberProperty, value); }
        }

        [DataMember(Name = "philhealthNumber")]
        public string PhilhealthNumber
        {
            get { return (string)GetValue(PhilhealthNumberProperty); }
            set { SetValue(PhilhealthNumberProperty, value); }
        }

        [DataMember(Name = "pagIbigIdNumber")]
        public string PagIbigIdNumber
        {
            get { return (string)GetValue(PagIbigIdNumberProperty); }
            set { SetValue(PagIbigIdNumberProperty, value); }
        }

        [DataMember(Name = "gsisIdNumber")]
        public string GsisIdNumber
        {
            get { return (string)GetValue(GsisIdNumberProperty); }
            set { SetValue(GsisIdNumberProperty, value); }
        }

        [DataMember(Name = "agencyId")]
        public string AgencyId
        {
            get { return (string)GetValue(AgencyIdProperty); }
            set { SetValue(AgencyIdProperty, value); }
        }
    }
}
