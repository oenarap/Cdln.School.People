using System.Collections.Generic;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Views.Attributes;

namespace Cdln.School.People.Uwp.Lists
{
    public static class PersonnelAttributesContextDescriptors
    {
        public static IEnumerable<AttributeContextDescriptor> Items { get; } = new List<AttributeContextDescriptor>()
        {
            new AttributeContextDescriptor("", "Personal Information", typeof(PersonalInformationView)),
            new AttributeContextDescriptor("", "Family Members", typeof(FamilyBackgroundView)),
            new AttributeContextDescriptor("", "Educational Background", typeof(EducationalBackgroundView)),
            new AttributeContextDescriptor("", "Civil Service Eligibilities", typeof(EligibilitiesView)),
            new AttributeContextDescriptor("", "Work Experience", typeof(WorkExperienceView)),
            new AttributeContextDescriptor("", "Civic + Voluntary Works", typeof(CivicWorksView)),
            new AttributeContextDescriptor("", "Training Programs", typeof(TrainingProgramsView)),
            new AttributeContextDescriptor("", "Skills + Other Information", typeof(OtherInformationView)),
            new AttributeContextDescriptor("", "Frequenty Asked Questions", typeof(FaqsView)),
            new AttributeContextDescriptor("", "Verification Details", typeof(VerificationDetailsView))
        };
    }
}
