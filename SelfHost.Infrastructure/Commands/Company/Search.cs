using System;
using System.Collections.Generic;

namespace SelfHost.Infrastructure.Commands.Company
{
    public class Search
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public IEnumerable<string> EmployeeJobTitles { get; set; }
    }
}