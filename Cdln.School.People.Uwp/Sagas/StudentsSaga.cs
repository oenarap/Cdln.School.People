using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Apps.Communication.Core;
using School.People.Core.DTOs;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Sagas
{
    public sealed class StudentsSaga : MessagingModel, IHandle<InsertStudentCommand>, IHandle<GetResponseReceivedEvent>
    {
        public async Task Handle(GetResponseReceivedEvent message)
        {
            if (message.Id != Id) { return; }
            
            try
            {
                var content = await message.Data.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Person>>(content);
                Hub.Dispatch(new AllStudentsAcquiredEvent(Id, data));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Handle(InsertStudentCommand message)
        {
            try
            {
                var data = message.Data;
                var uri = new Uri($"{ Url }{ message.Id }"); // TEMP
                var response = await ApiClient.Post(message.Id, uri, data).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();

                if (JsonConvert.DeserializeObject<Guid?>(content) is Guid id)
                {
                    var person = new Person(id, data.LastName, data.FirstName, data.MiddleName, data.NameExtension, data.Title);
                    Hub.Dispatch(new StudentInsertedEvent(message.Id, person));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            hub.RegisterHandler<StudentsSaga, InsertStudentCommand>(this);
            hub.RegisterHandler<StudentsSaga, GetResponseReceivedEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            hub.UnregisterHandler<StudentsSaga, InsertStudentCommand>(this);
            hub.UnregisterHandler<StudentsSaga, GetResponseReceivedEvent>(this);
        }

        public StudentsSaga(IMessageHub hub, IApiClient apiClient)
            : base(hub) {

            ApiClient = apiClient;
            Url = "https://localhost:44397/api/students/"; // TEMP
        }

        private readonly IApiClient ApiClient;
        private readonly string Url; // TEMP
    }
}
