using System;
using System.ComponentModel.DataAnnotations;

namespace SelfHost.Infrastructure.Commands.Company
{
    public class UpdateCompanyEmployee
    {
        [Required(ErrorMessage = "FristName is required.")]
        public string FristName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "JobTitle is required.")]
        public string JobTitle { get; set; }
    }
}