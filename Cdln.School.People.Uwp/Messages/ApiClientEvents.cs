using System;
using System.Net.Http;

namespace Cdln.School.People.Uwp.Messages
{
    public class TokenAcquiredEvent : Message<Guid?>
    {
        public TokenAcquiredEvent(Guid id, Guid? data)
            : base(id, data) { }
    }

    public class GetResponseReceivedEvent : Message<HttpResponseMessage>
    {
        public GetResponseReceivedEvent(Guid id, HttpResponseMessage data)
            : base(id, data) { }
    }

    public class RequestErrorEvent : Message<string>
    {
        public RequestErrorEvent(Guid id, string data)
            : base(id, data) { }
    }

    public class RequestTimedOutErrorEvent : Message<string>
    {
        public RequestTimedOutErrorEvent(Guid id)
            : base(id, "ERROR: Request has timed out.") { }
    }

    public class SericeStatusChangedEvent : Message<ServiceStatus>
    {
        public SericeStatusChangedEvent(ServiceStatus data)
            : base(Guid.Empty, data) { }
    }
}
