using System;

namespace Cdln.School.People.Uwp.Messages
{
    public sealed class CommentsQuery : Message<Guid>
    {
        public CommentsQuery(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetFamilyMembers : Message<Guid>
    {
        public GetFamilyMembers(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetPersonalInformation : Message<Guid>
    {
        public GetPersonalInformation(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetVerificationDetails : Message<Guid>
    {
        public GetVerificationDetails(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetFaqs : Message<Guid>
    {
        public GetFaqs(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetOtherInfo : Message<Guid>
    {
        public GetOtherInfo(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetCivicWorks : Message<Guid>
    {
        public GetCivicWorks(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetWorks : Message<Guid>
    {
        public GetWorks(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetTrainings : Message<Guid>
    {
        public GetTrainings(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetEligibilities : Message<Guid>
    {
        public GetEligibilities(Guid id, Guid param)
            : base(id, param) { }
    }

    public sealed class GetEducations : Message<Guid>
    {
        public GetEducations(Guid id, Guid param)
            : base(id, param) { }
    }
}
