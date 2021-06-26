using System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using System.Collections.ObjectModel;
using Cdln.School.People.Uwp.Messages;
using Cdln.School.People.Uwp.Views.Attributes;

namespace Cdln.School.People.Uwp.ViewModels.Attributes
{
    public class FamilyBackgroundViewModel : AttributeViewModel<FamilyBackgroundView>, IHandle<FamilyMembersAcquiredEvent>
    {
        private static readonly DependencyProperty FamilyMembersProperty = DependencyProperty.Register(nameof(FamilyMembers), typeof(ObservableCollection<RelatedPerson>), typeof(FamilyBackgroundViewModel), new PropertyMetadata(null));

        public ObservableCollection<RelatedPerson> FamilyMembers => (ObservableCollection<RelatedPerson>)GetValue(FamilyMembersProperty);


        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            base.RegisterHandledMessages(hub);
            hub.RegisterHandler<FamilyBackgroundViewModel, FamilyMembersAcquiredEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            base.UnregisterHandledMessages(hub);
            hub.UnregisterHandler<FamilyBackgroundViewModel, FamilyMembersAcquiredEvent>(this);
        }

        public async Task Handle(FamilyMembersAcquiredEvent message)
        {
            if (message.Id == Id && this.IsCurrent)
            {
                var family = new ObservableCollection<RelatedPerson>();

                foreach (var p in message.Data)
                {
                    family.Add(new RelatedPerson(p.Id, p.LastName, p.FirstName, p.MiddleName, p.NameExtension, p.Title, null));
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => SetValue(FamilyMembersProperty, family));
            }
        }

        protected override void RequestData(Guid id) => Hub.Dispatch(new GetFamilyMembers(Id, id));
        protected override void ResetValues() => SetValue(FamilyMembersProperty, default);

        protected override void SaveChanges(Guid id)
        {
            var modifiedMembers = new List<RelatedPerson>();

            foreach (var member in FamilyMembers)
            {
                if (member.IsModified)
                {
                    modifiedMembers.Add(member);
                }
            }

            Hub.Dispatch(new UpdateFamilyMembersCommand(id, modifiedMembers));
        }


        public FamilyBackgroundViewModel(IMessageHub hub, PeopleListViewModel people, AttributeContextsListViewModel attributeContexts)
            : base(hub, people, (attributeContexts.View?.CurrentItem as AttributeContextDescriptor)?.AssociatedViewType) { }
    }
}
