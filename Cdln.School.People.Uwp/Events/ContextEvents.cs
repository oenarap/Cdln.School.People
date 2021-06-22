using System;
using Cdln.School.People.Uwp.Models;

namespace Cdln.School.People.Uwp.Events
{
    public class PeopleContextChangedEvent : Event<PeopleContextDescriptor>
    {
        public PeopleContextChangedEvent(Guid id, PeopleContextDescriptor data)
            : base(id, data) { }
    }

    public class AttributeContextChangedEvent : Event<(AttributeContextDescriptor NewValue, AttributeContextDescriptor OldValue)>
    {
        public AttributeContextChangedEvent(Guid id, (AttributeContextDescriptor NewValue, AttributeContextDescriptor OldValue) data)
            : base(id, data) { }
    }

    public class NoCurrentAttributeContextEvent : Event
    {
        public NoCurrentAttributeContextEvent(Guid id)
            : base(id) { }
    }

    public class NoCurrentPeopleContextEvent : Event
    {
        public NoCurrentPeopleContextEvent(Guid id)
            : base(id) { }
    }
}
