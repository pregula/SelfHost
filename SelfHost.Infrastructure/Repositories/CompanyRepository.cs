using Microsoft.EntityFrameworkCore;
using SelfHost.DataAccess;
using SelfHost.Domain;
using SelfHost.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHost.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetAsync(long companyId)
        {
            return await Task.FromResult(_context.Companies.Include(c => c.Employees).SingleOrDefault(c => c.Id == companyId));
        }

        public async Task<long> AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<IEnumerable<Company>> BrowseAsync(DateTime? employeeDateOfBirthFrom, DateTime? employeeDateOfBirthTo, IEnumerable<JobTitleEnum> employeeJobTitles, string keyword = "")
        {
            var query = _context.Companies
                .Include(c => c.Employees)
                .AsNoTracking();

            if (employeeDateOfBirthFrom != null)
            {
                query = query.Where(c => c.Employees.Any(e => e.DateOfBirth >= employeeDateOfBirthFrom));
            }

            if (employeeDateOfBirthTo != null)
            {
                query = query.Where(c => c.Employees.Any(e => e.DateOfBirth <= employeeDateOfBirthTo));
            }

            if (employeeJobTitles.Count() > 0)
            {
                query = query.Where(c => c.Employees.Any(e => employeeJobTitles.Contains(e.JobTitle)));
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.Name.Contains(keyword)
                || c.Employees.Any(e => e.FirstName.Contains(keyword) || e.LastName.Contains(keyword)));
            }

            return await Task.FromResult(query.AsEnumerable());
        }

        public async Task DeleteAsync(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            await Task.FromResult(_context.Companies.Update(company));
            await _context.SaveChangesAsync();
        }
    }
}