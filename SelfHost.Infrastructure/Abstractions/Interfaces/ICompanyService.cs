using SelfHost.Infrastructure.Commands.Company;
using SelfHost.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelfHost.Infrastructure.Abstractions.Interfaces
{
    public interface ICompanyService
    {
        Task<long> CreateAsync(CreateCompany dto);

        Task AddEmployeesAsync(long companyId, IEnumerable<CreateCompanyEmployee> employees);

        Task<ResultDto> SearchAsync(Search dto);

        Task UpdateAsync(long id, UpdateCompany dto);

        Task DeleteAsync(long id);
    }
}