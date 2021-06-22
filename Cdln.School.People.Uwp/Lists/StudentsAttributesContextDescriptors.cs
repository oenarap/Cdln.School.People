using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Views.Attributes;

namespace Cdln.School.People.Uwp.Lists
{
    public static class StudentsAttributesContextDescriptors
    {
        public static IEnumerable<AttributeContextDescriptor> Items { get; } = new List<AttributeContextDescriptor>()
        {
            new AttributeContextDescriptor("", "Personal Information", typeof(PersonalInformationView)),
            new AttributeContextDescriptor("", "Family Members", typeof(FamilyBackgroundView)),
            new AttributeContextDescriptor("", "Educational Background", typeof(EducationalBackgroundView)),
            new AttributeContextDescriptor("", "Enrollment Information", null, null),
            new AttributeContextDescriptor("", "Skills + Other Information", typeof(OtherInformationView)),
            new AttributeContextDescriptor("", "Verification Details", typeof(VerificationDetailsView))
        };
    }
}
