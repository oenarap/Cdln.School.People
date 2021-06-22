using System;
using Windows.UI.Xaml;
using School.People.Core;
using System.Runtime.Serialization;

namespace Cdln.School.People.Uwp.Models
{
    [DataContract(Name = nameof(Person))]
    public class Person : Model, IPerson
    {
        public static readonly DependencyProperty LastNameProperty = DependencyProperty.Register(nameof(LastName), typeof(string), typeof(Person), new PropertyMetadata(null, OnLastNamePropertyChanged));
        public static readonly DependencyProperty FirstNameProperty = DependencyProperty.Register(nameof(FirstName), typeof(string), typeof(Person), new PropertyMetadata(null, OnFirstNamePropertyChanged));
        public static readonly DependencyProperty MiddleNameProperty = DependencyProperty.Register(nameof(MiddleName), typeof(string), typeof(Person), new PropertyMetadata(null, OnMiddleNamePropertyChanged));
        public static readonly DependencyProperty NameExtensionProperty = DependencyProperty.Register(nameof(NameExtension), typeof(string), typeof(Person), new PropertyMetadata(null, OnNameExtensionPropertyChanged));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(Person), new PropertyMetadata(null, OnTitlePropertyChanged));

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Person model = (Person)d;
            string str = (string)e.NewValue;
            model.Markers[M04] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnNameExtensionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Person model = (Person)d;
            string str = (string)e.NewValue;
            model.Markers[M03] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnMiddleNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Person model = (Person)d;
            string str = (string)e.NewValue;
            model.Markers[M02] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnFirstNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Person model = (Person)d;
            string str = (string)e.NewValue;
            model.Markers[M01] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        private static void OnLastNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Person model = (Person)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }

        public Person()
            : this(Guid.Empty, null, null, null) { }
        public Person(Guid id, string lastName, string firstName, string middleName)
            : this(id, lastName, firstName, middleName, null) { }
        public Person(Guid id, string lastName, string firstName, string middleName, string nameExtension)
            : this(id, lastName, firstName, middleName, nameExtension, null) { }
        public Person(Guid id, string lastName, string firstName, string middleName, string nameExtension, string title)
            : base(id)
        {
            SetValue(LastNameProperty, lastName);
            SetValue(FirstNameProperty, firstName);
            SetValue(MiddleNameProperty, middleName);
            SetValue(NameExtensionProperty, nameExtension);
            SetValue(TitleProperty, title);
            IsInitializing = false;
        }

        [DataMember(Name = "lastName")]
        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        [DataMember(Name = "firstName")]
        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        [DataMember(Name = "middleName")]
        public string MiddleName
        {
            get { return (string)GetValue(MiddleNameProperty); }
            set { SetValue(MiddleNameProperty, value); }
        }

        [DataMember(Name = "nameExtension")]
        public string NameExtension
        {
            get { return (string)GetValue(NameExtensionProperty); }
            set { SetValue(NameExtensionProperty, value); }
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
