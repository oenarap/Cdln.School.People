using System;
using School.People.Core.DTOs;
using System.Collections.Generic;

namespace Cdln.School.People.Uwp.Messages
{
    public class VerificationDetailsAcquiredEvent : Message<IEnumerable<VerificationDetailsAggregate>>
    {
        public VerificationDetailsAcquiredEvent(Guid id, IEnumerable<VerificationDetailsAggregate> data)
            : base(id, data) { }
    }

    public class FaqsAcquiredEvent : Message<IEnumerable<Faqs>>
    {
        public FaqsAcquiredEvent(Guid id, IEnumerable<Faqs> data)
            : base(id, data) { }
    }

    public class OtherInformationAcquiredEvent : Message<IEnumerable<OtherInformation>>
    {
        public OtherInformationAcquiredEvent(Guid id, IEnumerable<OtherInformation> data)
            : base(id, data) { }
    }

    public class TrainingsAcquiredEvent : Message<IEnumerable<Training>>
    {
        public TrainingsAcquiredEvent(Guid id, IEnumerable<Training> data)
            : base(id, data) { }
    }

    public class CivicWorksAcquiredEvent : Message<IEnumerable<CivicWork>>
    {
        public CivicWorksAcquiredEvent(Guid id, IEnumerable<CivicWork> data)
            : base(id, data) { }
    }

    public class WorksAcquiredEvent : Message<IEnumerable<Work>>
    {
        public WorksAcquiredEvent(Guid id, IEnumerable<Work> data)
            : base(id, data) { }
    }

    public class EligibilitiesAcquiredEvent : Message<IEnumerable<Eligibility>>
    {
        public EligibilitiesAcquiredEvent(Guid id, IEnumerable<Eligibility> data)
            : base(id, data) { }
    }

    public class EducationsAcquiredEvent : Message<IEnumerable<Education>>
    {
        public EducationsAcquiredEvent(Guid id, IEnumerable<Education> data)
            : base(id, data) { }
    }

    public class FamilyMembersAcquiredEvent : Message<IEnumerable<Person>>
    {
        public FamilyMembersAcquiredEvent(Guid id, IEnumerable<Person> data)
            : base(id, data) { }
    }

    public class PersonalInformationAcquiredEvent : Message<PersonalInformationAggregate>
    {
        public PersonalInformationAcquiredEvent(Guid id, PersonalInformationAggregate data)
            : base(id, data) { }
    }




    public class ArchivedPeopleAcquiredEvent : Message<IEnumerable<Person>>
    {
        public ArchivedPeopleAcquiredEvent(Guid id, IEnumerable<Person> data)
            : base(id, data) { }
    }

    public class OtherPeopleAcquiredEvent : Message<IEnumerable<Person>>
    {
        public OtherPeopleAcquiredEvent(Guid id, IEnumerable<Person> data)
            : base(id, data) { }
    }

    public class AllPersonnelAcquiredEvent : Message<IEnumerable<Person>>
    {
        public AllPersonnelAcquiredEvent(Guid id, IEnumerable<Person> data)
            : base(id, data) { }
    }

    public class AllStudentsAcquiredEvent : Message<IEnumerable<Person>>
    {
        public AllStudentsAcquiredEvent(Guid id, IEnumerable<Person> data)
            : base(id, data) { }
    }

    public class PeopleAcquiredEvent : Message<IEnumerable<Person>>
    {
        public PeopleContext Context { get; }

        public PeopleAcquiredEvent(Guid id, PeopleContext context, IEnumerable<Person> data)
            : base(id, data) { Context = context; }
    }

    public class OtherPersonAcquiredEvent : Message<Person>
    {
        public OtherPersonAcquiredEvent(Guid id, Person data)
            : base(id, data) { }
    }

    public class PersonnelAcquiredEvent : Message<Person>
    {
        public PersonnelAcquiredEvent(Guid id, Person data)
            : base(id, data) { }
    }

    public class StudentAcquiredEvent : Message<Person>
    {
        public StudentAcquiredEvent(Guid id, Person data)
            : base(id, data) { }
    }
}
