using System.Collections.Generic;

namespace SelfHost.Infrastructure.DTO
{
    public class CompanyDetailsDto : CompanyDto
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}