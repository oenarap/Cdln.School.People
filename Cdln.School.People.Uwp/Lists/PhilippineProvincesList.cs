using System.Collections.Generic;

namespace Cdln.School.People.Uwp.Lists
{
    public class PhilippineProvincesList
    {
        public IEnumerable<string> Provinces => _Provinces;

        private static readonly IEnumerable<string> _Provinces = new List<string>()
        {
            "Abra", "Agusan Del Norte", "Agusan Del Sur", "Aklan", "Albay", "Antique", "Apayao",
            "Aurora", "Basilan", "Bataan", "Batanes", "Batangas", "Benguet", "Biliran", "Bohol",
            "Bukidnon", "Bulacan", "Cagayan", "Camarines Norte", "Camarines Sur", "Camiguin", "Capiz",
            "Catanduanes", "Cavite", "Cebu", "Compostella Valley", "Cotabato", "Davao Del Norte",
            "Davao Del Sur", "Davao Occidental", "Davao Oriental", "Dinagat Islands", "Eastern Samar",
            "Guimaras", "Ifugao", "Ilocos Norte", "Ilocos Sur", "Iloilo", "Isabela", "Kalinga", "La Union",
            "Laguna", "Lanao Del Norte", "Lanao Del Sur", "Leyte", "Maguindanao", "Marinduque", "Masbate",
            "Misamis Occidental", "Misamis Oriental", "Mountain Province", "Negros Occidental", "Negros Oriental",
            "Northern Samar", "Nueva Ecija", "Nueva Vizcaya", "Occidental Mindoro", "Oriental Mindoro", "Palawan",
            "Pampanga", "Pangasinan", "Quezon", "Quirino", "Rizal", "Romblon", "Samar", "Sarangani", "Siquijor",
            "Sorsogon", "South Cotabato", "Southern Leyte", "Sultan Kudarat", "Sulu", "Surigao Del Norte",
            "Surigao Del Sur", "Tarlac", "Tawi-Tawi", "Zambales", "Zamboanga Del Norte", "Zamboanga Del Sur", "Zamboanga Sibugay"
        };
    }
}
