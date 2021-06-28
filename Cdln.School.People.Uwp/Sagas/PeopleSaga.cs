using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Apps.Communication.Core;
using School.People.Core.DTOs;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Sagas
{
    public sealed class PeopleSaga : MessagingModel, IHandle<GetPeopleQuery<PeopleContext>>, IHandle<GetResponseReceivedEvent>
    {
        public async Task Handle(GetResponseReceivedEvent message)
        {
            if (message.RequestorId != Id) { return; }

            try
            {
                var id = message.Id;

                if (Requests.ContainsKey(id))
                {
                    var context = Requests[id];
                    var content = await message.Data.Content.ReadAsStringAsync();
                    var people = JsonConvert.DeserializeObject<List<Person>>(content);

                    Requests.Remove(id);
                    Hub.Dispatch(new PeopleAcquiredEvent(id, context, people));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Handle(GetPeopleQuery<PeopleContext> message)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var requestId = Guid.NewGuid();
                var context = message.Data;

                switch (context)
                {
                    case PeopleContext.Archived:
                        ApiClient.Get(requestId, Id, new Uri($"{ Url }archived/"));
                        break;

                    case PeopleContext.Others:
                        ApiClient.Get(requestId, Id, new Uri($"{ Url }others/"));
                        break;

                    case PeopleContext.Personnel:
                        ApiClient.Get(requestId, Id, new Uri($"{ Url }personnel/"));
                        break;

                    case PeopleContext.Students:
                        ApiClient.Get(requestId, Id, new Uri($"{ Url }students/"));
                        break;

                    default: return;
                }

                Requests.Add(requestId, context);
            });
        }

        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            base.RegisterHandledMessages(hub);
            hub.RegisterHandler<PeopleSaga, GetPeopleQuery<PeopleContext>>(this);
            hub.RegisterHandler<PeopleSaga, GetResponseReceivedEvent>(this);

        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            base.UnregisterHandledMessages(hub);
            hub.UnregisterHandler<PeopleSaga, GetPeopleQuery<PeopleContext>>(this);
            hub.UnregisterHandler<PeopleSaga, GetResponseReceivedEvent>(this);
        }

        public PeopleSaga(IMessageHub hub, IApiClient apiClient)
            : base(hub)
        {
            ApiClient = apiClient;
            Url = "https://localhost:44397/api/"; // TEMP
            Requests = new Dictionary<Guid, PeopleContext>();
        }

        private readonly IApiClient ApiClient;
        private readonly string Url; // TEMP
        private readonly Dictionary<Guid, PeopleContext> Requests;
    }
}
