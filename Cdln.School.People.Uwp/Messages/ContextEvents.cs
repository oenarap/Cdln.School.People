using System;
using Cdln.School.People.Uwp.Models;

namespace Cdln.School.People.Uwp.Messages
{
    public class PeopleContextChangedEvent : Message<(PeopleContextDescriptor NewValue, PeopleContextDescriptor OldValue)>
    {
        public PeopleContextChangedEvent(Guid id, (PeopleContextDescriptor NewValue, PeopleContextDescriptor OldValue) data)
            : base(id, data) { }
    }

    public class AttributeContextChangedEvent : Message<(AttributeContextDescriptor NewValue, AttributeContextDescriptor OldValue)>
    {
        public AttributeContextChangedEvent(Guid id, (AttributeContextDescriptor NewValue, AttributeContextDescriptor OldValue) data)
            : base(id, data) { }
    }

    public class NoCurrentAttributeContextEvent : Message
    {
        public NoCurrentAttributeContextEvent(Guid id)
            : base(id) { }
    }

    public class NoCurrentPeopleContextEvent : Message
    {
        public NoCurrentPeopleContextEvent(Guid id)
            : base(id) { }
    }
}
