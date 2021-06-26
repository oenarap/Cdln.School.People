using System;
using School.People.Core;

namespace Cdln.School.People.Uwp.Messages
{
    public class OtherPersonArchivedEvent : Message<IPerson>
    {
        public OtherPersonArchivedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class PersonnelArchivedEvent : Message<IPerson>
    {
        public PersonnelArchivedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class StudentArchivedEvent : Message<IPerson>
    {
        public StudentArchivedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class OtherPersonInsertedEvent : Message<IPerson>
    {
        public OtherPersonInsertedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class PersonnelInsertedEvent : Message<IPerson>
    {
        public PersonnelInsertedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class StudentInsertedEvent : Message<IPerson>
    {
        public StudentInsertedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class PersonUpdatedEvent : Message<IPerson>
    {
        public PersonUpdatedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class CurrentPersonChangedEvent : Message<IPerson>
    {
        public CurrentPersonChangedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class PersonNameChangedEvent : Message<IPerson>
    {
        public PersonNameChangedEvent(Guid id, IPerson data)
            : base(id, data) { }
    }

    public class NoCurrentPersonEvent : Message
    {
        public NoCurrentPersonEvent(Guid id)
            : base(id) { }
    }
}
