using System;
using School.People.Core;

namespace Cdln.School.People.Uwp.Messages
{
    public sealed class GetAllArchived : Message
    {
        public GetAllArchived(Guid id)
            : base(id) { }
    }

    public sealed class GetAllOthers : Message
    {
        public GetAllOthers(Guid id)
            : base(id) { }
    }

    public sealed class GetAllPersonnel : Message
    {
        public GetAllPersonnel(Guid id)
            : base(id) { }
    }

    public sealed class GetAllStudentsQuery : Message
    {
        public GetAllStudentsQuery(Guid id)
            : base(id) { }
    }

    public sealed class GetPeopleByName : Message<IPerson>
    {
        public GetPeopleByName(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class GetPersonByName : Message<IPerson>
    {
        public GetPersonByName(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class GetPersonById : Message<Guid>
    {
        public GetPersonById(Guid id, Guid data)
            : base(id, data) { }
    }

    public sealed class GetPeopleQuery<T> : Message<T>
    {
        public GetPeopleQuery(Guid id, T data)
            : base(id, data) { }
    }
}
