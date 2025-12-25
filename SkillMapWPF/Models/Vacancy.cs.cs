using System;
using System.Collections.Generic;
using System.Text;

namespace SkillMapWPF.Models
{
    public class Vacancy
    {
        public string VacancyTitle { get; set; }
        public string VacancyDescription { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public int? RequiredExperience { get; set; }
        public string Location { get; set; }
        public int CompanyId { get; set; }
    }
}
