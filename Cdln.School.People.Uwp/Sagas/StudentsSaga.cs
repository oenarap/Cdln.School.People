using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Apps.Communication.Core;
using School.People.Core.DTOs;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Sagas
{
    public sealed class StudentsSaga : Saga, IHandle<InsertStudentCommand>, IHandle<GetAllStudentsQuery>, IHandle<GetResponseReceivedEvent>
    {
        public async Task Handle(GetResponseReceivedEvent message)
        {
            if (message.Id != Id) { return; }
            
            try
            {
                var content = await message.Data.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Person>>(content);
                MessageHub.Dispatch(new AllStudentsAcquiredEvent(Id, data));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task Handle(GetAllStudentsQuery message)
        {
            return new Task(() => ApiClient.Get(Id, new Uri(url), message.Id));
        }

        public async Task Handle(InsertStudentCommand message)
        {
            try
            {
                var data = message.Data;
                var uri = new Uri($"{ url }{ message.Id }"); // TEMP
                var response = await ApiClient.Post(message.Id, uri, data).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();

                if (JsonConvert.DeserializeObject<Guid?>(content) is Guid id)
                {
                    var person = new Person(id, data.LastName, data.FirstName, data.MiddleName, data.NameExtension, data.Title);
                    MessageHub.Dispatch(new StudentInsertedEvent(message.Id, person));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void RegisterHandledMessages(IMessageHub messageHub)
        {
            messageHub.RegisterHandler<StudentsSaga, InsertStudentCommand>(this);
            messageHub.RegisterHandler<StudentsSaga, GetAllStudentsQuery>(this); 
            messageHub.RegisterHandler<StudentsSaga, GetResponseReceivedEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub messageHub)
        {
            messageHub.UnregisterHandler<StudentsSaga, InsertStudentCommand>(this);
            messageHub.UnregisterHandler<StudentsSaga, GetAllStudentsQuery>(this);
            messageHub.UnregisterHandler<StudentsSaga, GetResponseReceivedEvent>(this);
        }

        public StudentsSaga(IMessageHub messageHub, IApiClient apiClient)
            : base(messageHub) {

            ApiClient = apiClient;
            url = "https://localhost:44397/api/students/"; // TEMP
        }

        private readonly IApiClient ApiClient;
        private readonly string url; // TEMP
    }
}
