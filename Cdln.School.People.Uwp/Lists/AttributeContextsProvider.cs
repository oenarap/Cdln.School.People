using System;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;
using Cdln.School.People.Uwp.Views.Attributes;

namespace Cdln.School.People.Uwp.Lists
{
    public class AttributeContextsProvider : MessagingModel, IContextProvider
    {
        private static readonly DependencyProperty ContextsProperty = DependencyProperty.Register(nameof(Contexts), typeof(ICollectionView), typeof(AttributeContextsProvider), new PropertyMetadata(null, OnContextsPropertyChanged));
        private static readonly DependencyProperty CurrentProperty = DependencyProperty.Register(nameof(Current), typeof(AttributeContextDescriptor), typeof(AttributeContextsProvider), new PropertyMetadata(null));

        public ICollectionView Contexts => (ICollectionView)GetValue(ContextsProperty);
        public AttributeContextDescriptor Current => (AttributeContextDescriptor)GetValue(CurrentProperty);


        private static void OnContextsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var provider = (AttributeContextsProvider)d;
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += provider.OnCurrentContextChanged;
                var index = Math.Min(view.Count, provider.Logger.GetLog(provider.Logkey));
                if (view.MoveCurrentToPosition(index)) { return; }
            }
            provider.Hub.Dispatch(new NoCurrentAttributeContextEvent(provider.Id));
        }

        private void OnCurrentContextChanged(object sender, object e)
        {
            var view = (ICollectionView)sender;
            if (view.CurrentItem is AttributeContextDescriptor newValue)
            {
                var oldValue = Current;
                SetValue(CurrentProperty, newValue);

                Logger.Log(Logkey, view.CurrentPosition);
                Hub.Dispatch(new AttributeContextChangedEvent(Id, (newValue, oldValue)));
            }
            else { Hub.Dispatch(new NoCurrentAttributeContextEvent(Id)); }
        }

        public AttributeContextsProvider(IMessageHub hub, IIndexLogger logger)
            : base(hub)
        {
            Logger = logger;
            Logkey = $"KEY_{nameof(AttributeContextsProvider)}";
            Logger.RegisterKey(Logkey);

            var viewSource = new CollectionViewSource() { Source = contexts };
            SetValue(ContextsProperty, viewSource.View);
        }

        private readonly string Logkey;
        private readonly IIndexLogger Logger;
        private static readonly List<AttributeContextDescriptor> contexts = new List<AttributeContextDescriptor>()
        {
            new AttributeContextDescriptor(AttributeContext.PersonalInformation, "Personal Information", typeof(PersonalInformationView), ""),
            new AttributeContextDescriptor(AttributeContext.FamilyMembers, "Family Members", typeof(FamilyBackgroundView), ""),
            new AttributeContextDescriptor(AttributeContext.Educations, "Educational Background", typeof(EducationalBackgroundView), ""),
            new AttributeContextDescriptor(AttributeContext.Eligibilities, "Civil Service Eligibilities", typeof(EligibilitiesView), ""),
            new AttributeContextDescriptor(AttributeContext.Works, "Work Experience", typeof(WorkExperienceView), ""),
            new AttributeContextDescriptor(AttributeContext.CivicWorks, "Civic + Voluntary Works", typeof(CivicWorksView), ""),
            new AttributeContextDescriptor(AttributeContext.Trainings, "Training Programs", typeof(TrainingProgramsView), ""),
            new AttributeContextDescriptor(AttributeContext.OtherInformation, "Skills + Other Information", typeof(OtherInformationView), ""),
            new AttributeContextDescriptor(AttributeContext.Faqs, "Frequenty Asked Questions", typeof(FaqsView), ""),
            new AttributeContextDescriptor(AttributeContext.VerificationDetails, "Verification Details", typeof(VerificationDetailsView), "")
        };
    }
}
