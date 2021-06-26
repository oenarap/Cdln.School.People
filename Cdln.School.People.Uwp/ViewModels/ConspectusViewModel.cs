using System;
using Windows.UI.Xaml;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.Email;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class ConspectusViewModel : DependencyObject, IHandle<CurrentPersonChangedEvent>, IHandle<PersonUpdatedEvent>, IHandle<NoCurrentPersonEvent>
    {
        private static readonly DependencyProperty PersonProperty = DependencyProperty.Register(nameof(Person), typeof(IPerson), typeof(ConspectusViewModel), new PropertyMetadata(null));
        private static readonly DependencyProperty BirthdateProperty = DependencyProperty.Register(nameof(Birthdate), typeof(DateOfBirth), typeof(ConspectusViewModel), new PropertyMetadata(null));
        private static readonly DependencyProperty ContactDetailsProperty = DependencyProperty.Register(nameof(ContactDetails), typeof(string), typeof(ConspectusViewModel), new PropertyMetadata(null));
        private static readonly DependencyProperty EmailRecipientProperty = DependencyProperty.Register(nameof(EmailRecipient), typeof(EmailRecipient), typeof(ConspectusViewModel), new PropertyMetadata(null));
        private static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(nameof(ImageUri), typeof(Uri), typeof(ConspectusViewModel), new PropertyMetadata(null));
        private static readonly DependencyProperty AttributesListProperty = DependencyProperty.Register(nameof(AttributesList), typeof(List<AttributeContextDescriptor>), typeof(ConspectusViewModel), new PropertyMetadata(null));

        public ConspectusViewModel(IMessageHub messageHub, PeopleListViewModel peopleListViewModel)
        {
            SetValue(PersonProperty, peopleListViewModel.View?.CurrentItem as IPerson);
            messageHub?.RegisterHandler<ConspectusViewModel, CurrentPersonChangedEvent>(this);
            messageHub?.RegisterHandler<ConspectusViewModel, PersonUpdatedEvent>(this);
            messageHub?.RegisterHandler<ConspectusViewModel, NoCurrentPersonEvent>(this);
        }

        public async Task Handle(NoCurrentPersonEvent message)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SetValue(PersonProperty, null);
                SetValue(BirthdateProperty, null);
                SetValue(ContactDetailsProperty, null);
                SetValue(ImageUriProperty, null);
            });
        }

        public async Task Handle(CurrentPersonChangedEvent message)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                IPerson person = message.Data;
                SetValue(PersonProperty, person);
                // TODO: set quick details
                SetValue(BirthdateProperty, null);
                SetValue(ContactDetailsProperty, null);
                SetValue(ImageUriProperty, null);

                // TEMP
                SetValue(EmailRecipientProperty, new EmailRecipient("someone@example.com"));
            });
        }

        public async Task Handle(PersonUpdatedEvent message)
        {
            if (message.Data is IPerson person)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (Person.Id == person.Id) { SetValue(PersonProperty, person); }
                });
            }
        }

        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }
        public EmailRecipient EmailRecipient
        {
            get { return (EmailRecipient)GetValue(EmailRecipientProperty); }
            set { SetValue(EmailRecipientProperty, value); }
        }
        public ContactDetails ContactDetails
        {
            get { return (ContactDetails)GetValue(ContactDetailsProperty); }
            set { SetValue(ContactDetailsProperty, value); }
        }
        public DateOfBirth Birthdate
        {
            get { return (DateOfBirth)GetValue(BirthdateProperty); }
            set { SetValue(BirthdateProperty, value); }
        }
        public IPerson Person
        {
            get { return (IPerson)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }
        public List<AttributeContextDescriptor> AttributesList
        {
            get { return (List<AttributeContextDescriptor>)GetValue(AttributesListProperty); }
            set { SetValue(AttributesListProperty, value); }
        }
    }
}
