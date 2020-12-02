using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SelfHost.Infrastructure.Commands.Company
{
    public class CreateCompany
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EstablishmentYear is required.")]
        public int? EstablishmentYear { get; set; }

        public IEnumerable<CreateCompanyEmployee> Employees { get; set; }
    }
}