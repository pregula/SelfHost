using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelfHost.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetAsync(long companyId);

        Task<long> AddAsync(Company company);

        Task UpdateAsync(Company company);

        Task DeleteAsync(Company company);

        Task<IEnumerable<Company>> BrowseAsync(DateTime? employeeDateOfBirthFrom, DateTime? employeeDateOfBirthTo, IEnumerable<JobTitleEnum> employeeJobTitles, string keyword = "");
    }
}