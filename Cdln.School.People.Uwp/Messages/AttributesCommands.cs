using System;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Models.Addresses;

namespace Cdln.School.People.Uwp.Messages
{
    public class SaveCommentsCommand : Message<Comments>
    {
        public SaveCommentsCommand(Guid id, Comments data)
            : base(id, data) { }
    }

    public class SaveBirthAddressCommand : Message<RootAddress>
    {
        public SaveBirthAddressCommand(Guid id, RootAddress data)
            : base(id, data) { }
    }

    public class SaveBirthdateCommand : Message<DateOfBirth>
    {
        public SaveBirthdateCommand(Guid id, DateOfBirth data)
            : base(id, data) { }
    }

    public class SaveAgencyMemberDetailsCommand : Message<AgencyMemberDetails>
    {
        public SaveAgencyMemberDetailsCommand(Guid id, AgencyMemberDetails data)
            : base(id, data) { }
    }
}
