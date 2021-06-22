using System;
using School.People.Core;

namespace Cdln.School.People.Uwp.Events
{
    public class CurrentPersonChangedEvent : Event<IPerson>
    {
        public CurrentPersonChangedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class PersonNameChangedEvent : Event<IPerson>
    {
        public PersonNameChangedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class NoCurrentPersonEvent : Event
    {
        public NoCurrentPersonEvent(Guid id)
            : base(id) { }
    }
}
