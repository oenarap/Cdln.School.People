using System.Collections.Generic;

namespace Cdln.School.People.Uwp.Lists
{
    public class PhilippineRegionsList
    {
        public IEnumerable<string> Countries => _Countries;

        private static readonly IEnumerable<string> _Countries = new List<string>()
        {
            "National Capital Region (NCR)", "Ilocos Region (Region I)", "Cordillera Administrative Region (CAR)",
            "Cagayan Valley (Region II)", "Central Luzon (Region III)", "Calabarzon (Region IV-A Southern Tagalog Mainland)",
            "Mimaropa (Region IV-B Southwestern Tagalog Region)", "Bicol Region (Region V)", "Western Visayas (Region VI)",
            "Central Visayas (Region VII)", "Eastern Visayas (Region VIII)", "Zamboanga Peninsula (Region IX)",
            "Northern Mindanao (Region X)", "Davao Region (Region XI)", "Soccsksargen (Region XII)", "Caraga Region (Region XIII)",
            "Bangsamoro Autonomous Region in Muslim Mindanao (BARMM)"
        };
    }
}
