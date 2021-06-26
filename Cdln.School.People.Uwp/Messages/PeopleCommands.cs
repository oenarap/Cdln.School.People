using System;
using System.Collections.Generic;
using School.People.Core;

namespace Cdln.School.People.Uwp.Messages
{
    public sealed class InsertOtherPersonCommand : Message<IPerson>
    {
        public InsertOtherPersonCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class UpdateOtherPersonCommand : Message<IPerson>
    {
        public UpdateOtherPersonCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class ArchiveOtherPersonCommand : Message<IPerson>
    {
        public ArchiveOtherPersonCommand(Guid id, IPerson data)
            : base(id, data) { }
    }



    public sealed class InsertPersonnelCommand : Message<IPerson>
    {
        public InsertPersonnelCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class UpdatePersonnelCommand : Message<IPerson>
    {
        public UpdatePersonnelCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class ArchivePersonnelCommand : Message<IPerson>
    {
        public ArchivePersonnelCommand(Guid id, IPerson data)
            : base(id, data) { }
    }



    public sealed class InsertStudentCommand : Message<IPerson>
    {
        public InsertStudentCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class UpdateStudentCommand : Message<IPerson>
    {
        public UpdateStudentCommand(Guid id, IPerson data)
            : base(id, data) { }
    }

    public sealed class ArchiveStudentCommand : Message<IPerson>
    {
        public ArchiveStudentCommand(Guid id, IPerson data)
            : base(id, data) { }
    }


    
    public sealed class UpdateFamilyMembersCommand : Message<IEnumerable<IPerson>>
    {
        public UpdateFamilyMembersCommand(Guid id, IEnumerable<IPerson> data)
            : base(id, data) { }
    }
}
