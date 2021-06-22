using System;
using Windows.UI.Xaml;
using School.People.Core;

namespace Cdln.School.People.Uwp.Models
{
    public class RelatedPerson : Person, IRelatedPerson
    {
        public static readonly DependencyProperty RelationshipProperty = DependencyProperty.Register(nameof(Relationship), typeof(Relationship?), typeof(RelatedPerson), new PropertyMetadata(null, OnRelationshipPropertyChanged));

        private static void OnRelationshipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RelatedPerson model = (RelatedPerson)d;
            Relationship? rel = (Relationship?)e.NewValue;
            model.Markers[M05] = rel == null;
            model.Markers[Default] = model.IsInitializing;
        }

        public RelatedPerson()
            : this(Guid.Empty, null, null, null, null) { }
        public RelatedPerson(Guid id, string lastName, string firstName, string middleName, Relationship? relationship)
            : this(id, lastName, firstName, middleName, null, relationship) { }
        public RelatedPerson(Guid id, string lastName, string firstName, string middleName, string nameExtension, Relationship? relationship)
            : this(id, lastName, firstName, middleName, nameExtension, null, relationship) { }
        public RelatedPerson(Guid id, string lastName, string firstName, string middleName, string nameExtension, string title, Relationship? relationship)
            : base(id, lastName, firstName, middleName, nameExtension, title)
        {
            IsInitializing = true;
            SetValue(RelationshipProperty, relationship);
            IsInitializing = false;
        }

        public Relationship? Relationship
        {
            get { return (Relationship?)GetValue(RelationshipProperty); }
            set { SetValue(RelationshipProperty, value); }
        }
    }
}
