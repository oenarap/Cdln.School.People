using System;
using System.Net.Http;

namespace Cdln.School.People.Uwp.Events
{
    public class PostResponseReceivedEvent : Event<HttpResponseMessage>
    {
        public PostResponseReceivedEvent(Guid id, HttpResponseMessage data)
            : base(id, data) { }
    }

    public class PutResponseReceivedEvent : Event<HttpResponseMessage>
    {
        public PutResponseReceivedEvent(Guid id, HttpResponseMessage data)
            : base(id, data) { }
    }

    public class DeleteResponseReceivedEvent : Event<HttpResponseMessage>
    {
        public DeleteResponseReceivedEvent(Guid id, HttpResponseMessage data)
            : base(id, data) { }
    }

    public class GetResponseReceivedEvent : Event<HttpResponseMessage>
    {
        public GetResponseReceivedEvent(Guid id, HttpResponseMessage data)
            : base(id, data) { }
    }

    public class RequestErrorEvent : Event<string>
    {
        public RequestErrorEvent(Guid id, string data)
            : base(id, data) { }
    }
}
