using System;
using Windows.UI.Core;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using System.Collections.ObjectModel;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Lists
{
    public sealed partial class PeopleListProvider : MessagingModel, IHandle<PeopleContextChangedEvent>, IHandle<PeopleAcquiredEvent>,
        IHandle<PersonUpdatedEvent>, IHandle<PersonnelInsertedEvent>, IHandle<StudentInsertedEvent>, IHandle<PersonnelArchivedEvent>,
        IHandle<StudentArchivedEvent>
    {
        public Task Handle(StudentArchivedEvent message)
        {
            throw new NotImplementedException();
        }

        public Task Handle(PersonnelArchivedEvent message)
        {
            throw new NotImplementedException();
        }

        public Task Handle(StudentInsertedEvent message)
        {
            throw new NotImplementedException();
        }

        public Task Handle(PersonnelInsertedEvent message)
        {
            throw new NotImplementedException();
        }

        public Task Handle(PersonUpdatedEvent message)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(PeopleAcquiredEvent message)
        {
            if (message.Id != Id) { return; }

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var key = message.Context.ToString();
                var people = new ObservableCollection<IPerson>(message.Data);

                if (Cache.ContainsKey(key)) { Cache[key] = people; }
                else { Cache.Add(key, people); }

                if (ContextKey == key) { SetPeoplePropetyValue(people); }
            });
        }

        public async Task Handle(PeopleContextChangedEvent message)
        {
            if (message.Data.NewValue is PeopleContextDescriptor context)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var key = context.Type.ToString();

                    if (Cache.ContainsKey(key)) 
                    { 
                        SetPeoplePropetyValue(Cache[key]);
                        return;
                    }

                    Logger.RegisterKey(key);
                    ContextKey = key;
                    Hub.Dispatch(new GetPeopleQuery<PeopleContext>(Id, context.Type));
                });
            }
        }

        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            hub.RegisterHandler<PeopleListProvider, PeopleContextChangedEvent>(this);
            hub.RegisterHandler<PeopleListProvider, PeopleAcquiredEvent>(this);
            hub.RegisterHandler<PeopleListProvider, PersonUpdatedEvent>(this);
            hub.RegisterHandler<PeopleListProvider, PersonnelInsertedEvent>(this);
            hub.RegisterHandler<PeopleListProvider, StudentInsertedEvent>(this);
            hub.RegisterHandler<PeopleListProvider, PersonnelArchivedEvent>(this);
            hub.RegisterHandler<PeopleListProvider, StudentArchivedEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            hub.UnregisterHandler<PeopleListProvider, PeopleContextChangedEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, PeopleAcquiredEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, PersonUpdatedEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, PersonnelInsertedEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, StudentInsertedEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, PersonnelArchivedEvent>(this);
            hub.UnregisterHandler<PeopleListProvider, StudentArchivedEvent>(this);
        }

        public PeopleListProvider(IMessageHub hub, IIndexLogger logger)
            : base(hub) 
        { 
            Logger = logger;
            Cache = new Dictionary<string, ObservableCollection<IPerson>>();
        }
    }
}
